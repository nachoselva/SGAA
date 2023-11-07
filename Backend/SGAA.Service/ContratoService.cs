namespace SGAA.Service
{
    using Microsoft.AspNetCore.Identity;
    using SGAA.Documents.Contracts;
    using SGAA.Documents.DocumentModels;
    using SGAA.Domain.Auth;
    using SGAA.Domain.Core;
    using SGAA.Domain.Errors;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
    using SGAA.Models;
    using SGAA.Models.Mappers;
    using SGAA.Repository;
    using SGAA.Repository.Contracts;
    using SGAA.Service.Contracts;
    using SGAA.Utils.Configuration;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using System.Web;

    public class ContratoService : IContratoService
    {
        private readonly ISGAAConfiguration _configuration;
        private readonly IPagoService _pagoService;
        private readonly IContratoRepository _contratoRepository;
        private readonly IPostulacionRepository _postulacionRepository;
        private readonly IContratoMapper _contratoMapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly IContratoDocumentHandler _contratoDocumentHandler;
        private readonly IUsuarioInvitadoEmailSender _usuarioInvitadoEmailSender;
        private readonly IFirmaPendienteEmailSender _firmaPendienteEmailSender;
        private readonly IContratoEjecutadoEmailSender _contratoEjecutadoEmailSender;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IContratoCanceladoEmailSender _contratoCanceladoEmailSender;

        public ContratoService(ISGAAConfiguration configuration, IPagoService pagoService, IContratoRepository contratoRepository,
            IPostulacionRepository postulacionRepository, IContratoMapper contratoMapper, UserManager<Usuario> userManager,
            IContratoDocumentHandler contratoDocumentHandler, IUsuarioInvitadoEmailSender usuarioInvitadoEmailSender,
            IFirmaPendienteEmailSender firmaPendienteEmailSender, IContratoEjecutadoEmailSender contratoEjecutadoEmailSender,
            IContratoCanceladoEmailSender contratoCanceladoEmailSender, IUsuarioRepository usuarioRepository)
        {
            _configuration = configuration;
            _pagoService = pagoService;
            _contratoRepository = contratoRepository;
            _postulacionRepository = postulacionRepository;
            _contratoMapper = contratoMapper;
            _userManager = userManager;
            _contratoDocumentHandler = contratoDocumentHandler;
            _usuarioInvitadoEmailSender = usuarioInvitadoEmailSender;
            _firmaPendienteEmailSender = firmaPendienteEmailSender;
            _contratoEjecutadoEmailSender = contratoEjecutadoEmailSender;
            _contratoCanceladoEmailSender = contratoCanceladoEmailSender;
            _usuarioRepository = usuarioRepository;
        }
        private bool CanUsuarioFirmar(Usuario usuario, Contrato contrato)
        {
            return contrato.Status == ContratoStatus.FirmaPendiente &&
                contrato.Firmas.Any(f => f.UsuarioId == usuario.Id && !f.FechaFirma.HasValue);
        }

        private string GetArchivoContrato(Contrato contrato, int orderRenovacion)
        {
            static FirmaContratoDocumentModel BuildFirmaDocumentModel(Firma firma)
            {
                return new FirmaContratoDocumentModel
                {
                    DireccionIP = firma.DireccionIp,
                    FechaFirma = firma.FechaFirma?.ToShortDateString(),
                    NombreCompleto = firma.Usuario.NombreCompleto,
                    Domicilio = firma.Domicilio,
                    NumeroIdentificacion = firma.NumeroIdentificacion,
                    Rol = firma.Rol.ToString()
                };
            }

            DateOnly fechaDesde = contrato.FechaDesde;
            DateOnly fechaHasta = contrato.FechaHasta;
            IReadOnlyCollection<Firma> firmas = contrato.Firmas; ;
            Postulacion postulacion = contrato.Postulacion;
            string body = _contratoDocumentHandler.GetDocumentBody(
                            new ContratoDocumentModel
                            {
                                DomicilioCompleto = postulacion.Publicacion.Unidad.DomicilioCompleto,
                                Ciudad = postulacion.Publicacion.Unidad.Propiedad.Ciudad.Nombre,
                                Provincia = postulacion.Publicacion.Unidad.Propiedad.Ciudad.Provincia.Nombre,
                                FechaDesde = fechaDesde.ToShortDateString(),
                                FechaHasta = fechaHasta.ToShortDateString(),
                                FechaOferta = postulacion.FechaOferta!.Value.ToShortDateString(),
                                MontoAlquiler = postulacion.Publicacion.MontoAlquiler,
                                FirmadoFecha = null,
                                FirmasInquilinos = firmas.Where(f => f.Rol == FirmaRol.Inquilino).Select(BuildFirmaDocumentModel).ToList(),
                                FirmasPropietarios = firmas.Where(f => f.Rol == FirmaRol.Propietario).Select(BuildFirmaDocumentModel).ToList()
                            }
                        );
            var file = new
            {
                name = $"Contrato_Unidad_{postulacion.Id}_orderRenovacion.pdf",
                type = "application/pdf",
                size = 3 * (body.Length / 4),
                base64 = "data:application/pdf;base64," + body
            };
            return JsonSerializer.Serialize(file);
        }

        private async Task<ContratoGetModel> CreateContratoInternal(Postulacion postulacion, int orderRenovacion, DateOnly fechaDesde, DateOnly fechaHasta, decimal montoAlquiler)
        {
            async Task<Firma> BuildFirma(Persona postulante, FirmaRol rol)
            {
                Usuario? usuario = await _userManager.FindByEmailAsync(postulante.Email);
                if (usuario == null)
                {
                    usuario ??= new Usuario(postulante.Email, postulante.Nombre, postulante.Apellido, null, null, null, Licencia.HabilitacionProfesional) { UserName = postulante.Email, NormalizedEmail = postulante.Email };
                    var createUserResult = await _userManager.CreateAsync(usuario);
                    if (!createUserResult.Succeeded)
                        throw _userManager.MapIdentityErrorToBadRequest(createUserResult.Errors);
                    string resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                    string invitacionURL = $"{_configuration.Frontend.Url}/Usuario/reset-password?email={HttpUtility.UrlEncode(usuario.Email)}&token={HttpUtility.UrlEncode(resetPasswordToken)}";
                    await _usuarioInvitadoEmailSender.SendEmail(usuario.Email!,
                        new UsuarioInvitadoEmailModel
                        {
                            Nombre = usuario.Nombre,
                            Apellido = usuario.Apellido,
                            InvitacionURL = invitacionURL
                        }); ;
                }
                Firma firma = new(0, 0, null, null, rol, postulante.NumeroIdentificacion, postulante.Domicilio)
                {
                    UsuarioId = usuario.Id,
                    Usuario = usuario
                };
                return firma;
            }

            List<Firma> firmas = new();
            foreach (var postulante in postulacion.Aplicacion.Postulantes)
            {
                firmas.Add(await BuildFirma(postulante, FirmaRol.Inquilino));
            }
            foreach (var titular in postulacion.Publicacion.Unidad.Titulares)
            {
                firmas.Add(await BuildFirma(titular, FirmaRol.Propietario));
            }
            Contrato contrato = new(postulacion.Id, fechaDesde, fechaHasta, null, montoAlquiler, orderRenovacion, string.Empty, ContratoStatus.FirmaPendiente);
            contrato.AddFirmas(firmas);
            contrato.Postulacion = postulacion;
            contrato.Archivo = GetArchivoContrato(contrato, orderRenovacion);

            contrato = await _contratoRepository.AddContrato(contrato);

            foreach (var firma in contrato.Firmas)
            {
                Usuario firmaUsuario = firma.Usuario;
                await _firmaPendienteEmailSender.SendEmail(firmaUsuario.Email!,
                            new FirmaPendienteEmailModel
                            {
                                Domicilio = postulacion.Publicacion.Unidad.DomicilioCompleto,
                                Nombre = firmaUsuario.Nombre,
                                Apellido = firmaUsuario.Apellido
                            }); ;
            }

            return _contratoMapper.ToGetModel(contrato);
        }

        public async Task<ContratoGetModel> AddContrato(ContratoPostModel model)
        {
            DateOnly fechaDesde = new DateOnly(model.FechaDesde.Year, model.FechaDesde.Month, model.FechaDesde.Day);
            DateOnly fechaHasta = new DateOnly(model.FechaHasta.Year, model.FechaHasta.Month, model.FechaHasta.Day);
            Postulacion? postulacion = await _postulacionRepository.GetPostulacion(model.PostulacionId);
            if (postulacion == null)
                throw new NotFoundException();
            if (fechaDesde >= fechaHasta)
                throw new BadRequestException(nameof(model.FechaHasta), "Fecha hasta desde ser posterior a fecha desde");
            if (fechaDesde.AddYears(1) < fechaHasta)
                throw new BadRequestException(nameof(model.FechaHasta), "El contrato debe tener 1 año de duración como mínimo");
            if (fechaDesde < postulacion.Publicacion.InicioAlquiler)
                throw new BadRequestException(nameof(model.FechaDesde), "El contrato debe arrancar después de la fecha de inicio de la publicación");

            return await CreateContratoInternal(postulacion, 1, fechaDesde, fechaHasta, postulacion.Publicacion.MontoAlquiler);
        }

        public async Task<ContratoGetModel> GetContrato(int usuarioId, int contratoId)
        {
            Contrato? contrato = await _contratoRepository.GetContrato(contratoId);
            if (contrato == null || !contrato.Firmas.Any(f => f.UsuarioId == usuarioId))
                throw new NotFoundException();
            ContratoGetModel model = _contratoMapper.ToGetModel(contrato);
            Usuario usuario = (await _usuarioRepository.GetUsuarioById(usuarioId))!;
            model.CanUsuarioFirmar = CanUsuarioFirmar(usuario, contrato);
            return model; 
        }

        public async Task<ContratoGetModel> GetContrato(int contratoId)
        {
            Contrato? contrato = await _contratoRepository.GetContrato(contratoId);
            return contrato != null ?
                _contratoMapper.ToGetModel(contrato) :
                throw new NotFoundException();

        }

        public async Task<IReadOnlyCollection<ContratoGetModel>> GetContratos()
        {
            IReadOnlyCollection<Contrato> contratos = await _contratoRepository.GetContratos();
            return contratos.Select(c => _contratoMapper.ToGetModel(c)).ToList();
        }

        public async Task<IReadOnlyCollection<ContratoGetModel>> GetContratos(int usuarioId)
        {
            Usuario usuario = (await _usuarioRepository.GetUsuarioById(usuarioId))!;
            IReadOnlyCollection<Contrato> contratos = await _contratoRepository.GetContratos(usuarioId);
            return contratos.Select(c =>
            {
                ContratoGetModel model = _contratoMapper.ToGetModel(c);
                model.CanUsuarioFirmar = CanUsuarioFirmar(usuario, c);
                return model;
            }).ToList();
        }

        public async Task<ContratoGetModel> FirmarContrato(int usuarioId, int contratoId, string direccionIp)
        {
            Contrato? contrato = await _contratoRepository.GetContrato(contratoId);
            Firma? firmaActual = (contrato?.Firmas.FirstOrDefault(f => f.UsuarioId == usuarioId)) ?? throw new NotFoundException();
            if (contrato.Status != ContratoStatus.FirmaPendiente)
                throw new BadRequestException(nameof(contrato.Status), "Este contrato no se encuentra en estado para firmar");
            if (firmaActual.FechaFirma.HasValue)
                throw new BadRequestException(nameof(firmaActual.FechaFirma), "Este contrato ya fue firmado por el usuario");
            firmaActual.DireccionIp = direccionIp;
            firmaActual.FechaFirma = DateTime.Now;
            contrato.Archivo = GetArchivoContrato(contrato, 1);
            if (contrato.Firmas.All(f => f.FechaFirma.HasValue))
            {
                contrato.Status = ContratoStatus.Ejecutado;
            }
            contrato = await _contratoRepository.UpdateContrato(contrato);
            if (contrato.Status == ContratoStatus.Ejecutado)
            {
                foreach (var firma in contrato.Firmas)
                {
                    Usuario firmaUsuario = firma.Usuario;
                    await _contratoEjecutadoEmailSender.SendEmail(firmaUsuario.Email!,
                                new ContratoEjecutadoEmailModel
                                {
                                    Domicilio = contrato.Postulacion.Publicacion.Unidad.DomicilioCompleto,
                                    Nombre = firmaUsuario.Nombre,
                                    Apellido = firmaUsuario.Apellido
                                });
                }
                await _pagoService.CreatePagosContrato(contrato.Id);
            }

            return _contratoMapper.ToGetModel(contrato);
        }

        public async Task<ContratoGetModel> RenovarContrato(int contratoId, RenovarContratoPostModel model)
        {
            Contrato currentContrato = await _contratoRepository.GetContrato(contratoId)
                ?? throw new NotFoundException();
            if (currentContrato.Status != ContratoStatus.Ejecutado)
                throw new BadRequestException(nameof(currentContrato.Status), "El contrato no se encuentra en estado para ser renovado");
            DateOnly fechaDesde = currentContrato.FechaHasta.AddDays(1);
            if (fechaDesde >= model.FechaHasta)
                throw new BadRequestException(nameof(model.FechaHasta), "Fecha hasta desde ser posterior a fecha desde");
            int orderRenovacion = currentContrato.OrdenRenovacion + 1;
            Postulacion postulacion = currentContrato.Postulacion;
            currentContrato.Status = ContratoStatus.Renovado;
            await _contratoRepository.UpdateContrato(currentContrato);
            return await CreateContratoInternal(postulacion, orderRenovacion, fechaDesde, model.FechaHasta, model.MontoAlquiler);
        }

        public async Task<ContratoGetModel> CancelarContrato(int contratoId, CancelarContratoPutModel model)
        {
            Contrato currentContrato = await _contratoRepository.GetContrato(contratoId)
                ?? throw new NotFoundException();
            if (currentContrato.Status != ContratoStatus.Ejecutado)
                throw new BadRequestException(nameof(currentContrato.Status), "El contrato no se encuentra en estado para ser cancelar");
            currentContrato = model.ToEntity(_contratoMapper, currentContrato);
            currentContrato = await _contratoRepository.UpdateContrato(currentContrato);

            foreach (var firma in currentContrato.Firmas)
            {
                Usuario firmaUsuario = firma.Usuario;
                await _contratoCanceladoEmailSender.SendEmail(firmaUsuario.Email!,
                            new ContratoCanceladoEmailModel
                            {
                                Domicilio = currentContrato.Postulacion.Publicacion.Unidad.DomicilioCompleto,
                                Nombre = firmaUsuario.Nombre,
                                Apellido = firmaUsuario.Apellido,
                                FechaCancelacion = currentContrato.FechaCancelacion!.Value.ToShortDateString()
                            });
            }

            return currentContrato.MapToGetModel(_contratoMapper);
        }
    }
}
