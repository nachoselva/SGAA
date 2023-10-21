namespace SGAA.Service
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using SGAA.Documents.Contracts;
    using SGAA.Documents.DocumentModels;
    using SGAA.Domain.Auth;
    using SGAA.Domain.Core;
    using SGAA.Domain.Errors;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
    using SGAA.Models;
    using SGAA.Models.Mappers;
    using SGAA.Repository.Contracts;
    using SGAA.Service.Contracts;
    using SGAA.Utils.Configuration;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web;

    public class ContratoService : IContratoService
    {
        private readonly ISGAAConfiguration _configuration;
        private readonly IContratoRepository _contratoRepository;
        private readonly IPostulacionRepository _postulacionRepository;
        private readonly IContratoMapper _contratoMapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly IContratoDocumentHandler _contratoDocumentHandler;
        private readonly IUsuarioInvitadoEmailSender _usuarioInvitadoEmailSender;
        private readonly IFirmaPendienteEmailSender _firmaPendienteEmailSender;
        private readonly IContratoEjecutadoEmailSender _contratoEjecutadoEmailSender;

        public ContratoService(ISGAAConfiguration configuration, IContratoRepository contratoRepository, IPostulacionRepository postulacionRepository,
            IContratoMapper contratoMapper, UserManager<Usuario> userManager, IContratoDocumentHandler contratoDocumentHandler,
            IUsuarioInvitadoEmailSender usuarioInvitadoEmailSender, IFirmaPendienteEmailSender firmaPendienteEmailSender,
            IContratoEjecutadoEmailSender contratoEjecutadoEmailSender)
        {
            _configuration = configuration;
            _contratoRepository = contratoRepository;
            _postulacionRepository = postulacionRepository;
            _contratoMapper = contratoMapper;
            _userManager = userManager;
            _contratoDocumentHandler = contratoDocumentHandler;
            _usuarioInvitadoEmailSender = usuarioInvitadoEmailSender;
            _firmaPendienteEmailSender = firmaPendienteEmailSender;
            _contratoEjecutadoEmailSender = contratoEjecutadoEmailSender;
        }

        private byte[] GetArchivoContrato(Contrato contrato)
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
                    Rol = firma.Rol.ToString(),
                    TipoIdentificacion = firma.TipoIdentificacion.ToString()
                };
            }

            DateOnly fechaDesde = contrato.FechaDesde;
            DateOnly fechaHasta = contrato.FechaHasta;
            IReadOnlyCollection<Firma> firmas = contrato.Firmas; ;
            Postulacion postulacion = contrato.Postulacion;
            return _contratoDocumentHandler.GetDocumentBody(
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
        }

        public async Task<ContratoGetModel> CreateContrato(int postulacionId, DateOnly fechaDesde, DateOnly fechaHasta)
        {
            async Task<Firma> BuildFirma(Persona postulante, FirmaRol rol)
            {
                Usuario? usuario = await _userManager.FindByEmailAsync(postulante.Email);
                if (usuario == null)
                {
                    usuario ??= new Usuario(postulante.Email, postulante.Nombre, postulante.Apellido, null, null, null) { UserName = postulante.Email, NormalizedEmail = postulante.Email };
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
                Firma firma = new(0, 0, null, null, rol, postulante.TipoIdentificacion, postulante.NumeroIdentificacion, postulante.Domicilio)
                {
                    UsuarioId = usuario.Id,
                    Usuario = usuario
                };
                return firma;
            }

            Postulacion postulacion = (await _postulacionRepository.GetPostulacionById(postulacionId))!;
            List<Firma> firmas = new();
            foreach (var postulante in postulacion.Aplicacion.Postulantes)
            {
                firmas.Add(await BuildFirma(postulante, FirmaRol.Inquilino));
            }
            foreach (var titular in postulacion.Publicacion.Unidad.Titulares)
            {
                firmas.Add(await BuildFirma(titular, FirmaRol.Propietario));
            }
            Contrato contrato = new(fechaDesde, fechaHasta, null, postulacion.Publicacion.MontoAlquiler, 0, Array.Empty<byte>(), ContratoStatus.FirmaPendiente);
            contrato.AddFirmas(firmas);
            contrato.Postulacion = postulacion;
            contrato.Archivo = GetArchivoContrato(contrato);

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

        public async Task<ContratoGetModel?> GetContrato(int usuarioId, int contratoId)
        {
            Contrato? contrato = await _contratoRepository.GetContrato(contratoId);
            return contrato != null && contrato.Firmas.Any(f => f.UsuarioId == usuarioId) ?
                _contratoMapper.ToGetModel(contrato) :
                throw new NotFoundException();
        }

        public async Task<ContratoGetModel?> GetContratoAdmin(int contratoId)
        {
            Contrato? contrato = await _contratoRepository.GetContrato(contratoId);
            return contrato != null ?
                _contratoMapper.ToGetModel(contrato) :
                throw new NotFoundException();
        }

        public async Task<IReadOnlyCollection<ContratoGetModel>> GetContratosAdmin()
        {
            IReadOnlyCollection<Contrato> contratos = await _contratoRepository.GetContratosAdmin();
            return contratos.Select(c => _contratoMapper.ToGetModel(c)).ToList();
        }

        public async Task<IReadOnlyCollection<ContratoGetModel>> GetContratos(int usuarioId)
        {
            IReadOnlyCollection<Contrato> contratos = await _contratoRepository.GetContratosByUsuarioId(usuarioId);
            return contratos.Select(c => _contratoMapper.ToGetModel(c)).ToList();
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
            contrato.Archivo = GetArchivoContrato(contrato);
            if (contrato.Firmas.All(f => f.FechaFirma.HasValue))
            {
                contrato.Status = ContratoStatus.Ejecutado;
            }
            contrato = await _contratoRepository.UpdateContrato(contrato);
            if (contrato.Status == ContratoStatus.Ejecutado)
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
            return _contratoMapper.ToGetModel(contrato);
        }
    }
}
