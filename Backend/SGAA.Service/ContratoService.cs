namespace SGAA.Service
{
    using Microsoft.AspNetCore.Identity;
    using SGAA.Documents.Contracts;
    using SGAA.Documents.DocumentModels;
    using SGAA.Domain.Auth;
    using SGAA.Domain.Core;
    using SGAA.Domain.Errors;
    using SGAA.Models;
    using SGAA.Models.Mappers;
    using SGAA.Repository.Contracts;
    using SGAA.Service.Contracts;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ContratoService : IContratoService
    {
        private readonly IContratoRepository _contratoRepository;
        private readonly IPostulacionRepository _postulacionRepository;
        private readonly IContratoMapper _contratoMapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly IContratoDocumentHandler _contratoDocumentHandler;

        public ContratoService(IContratoRepository contratoRepository, IPostulacionRepository postulacionRepository,
            IContratoMapper contratoMapper, UserManager<Usuario> userManager, IContratoDocumentHandler contratoDocumentHandler)
        {
            _contratoRepository = contratoRepository;
            _postulacionRepository = postulacionRepository;
            _contratoMapper = contratoMapper;
            _userManager = userManager;
            _contratoDocumentHandler = contratoDocumentHandler;
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
            Contrato contrato = new(fechaDesde, fechaHasta, null, postulacion.Publicacion.MontoAlquiler, 0, new byte[0], ContratoStatus.FirmaPendiente);
            contrato.AddFirmas(firmas);
            contrato.Postulacion = postulacion;

            contrato.Archivo = _contratoDocumentHandler.GetDocumentBody(
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
                    FirmasInquilinos = firmas.Where(f => f.Rol == FirmaRol.Inquilino).Select(f =>
                    new FirmaContratoDocumentModel
                    {
                        DireccionIP = f.DireccionIp,
                        FechaFirma = f.FechaFirma?.ToShortDateString(),
                        NombreCompleto = f.Usuario.NombreCompleto,
                        Domicilio = f.Domicilio,
                        NumeroIdentificacion = f.NumeroIdentificacion,
                        Rol = f.Rol.ToString(),
                        TipoIdentificacion = f.TipoIdentificacion.ToString()
                    }).ToList(),
                    FirmasPropietarios = firmas.Where(f => f.Rol == FirmaRol.Propietario).Select(f =>
                    new FirmaContratoDocumentModel
                    {
                        DireccionIP = f.DireccionIp,
                        FechaFirma = f.FechaFirma?.ToShortDateString(),
                        NombreCompleto = f.Usuario.NombreCompleto,
                        Domicilio = f.Domicilio,
                        NumeroIdentificacion = f.NumeroIdentificacion,
                        Rol = f.Rol.ToString(),
                        TipoIdentificacion = f.TipoIdentificacion.ToString()
                    }).ToList()
                }
            );

            contrato = await _contratoRepository.AddContrato(contrato);

            return _contratoMapper.ToGetModel(contrato);

        }

        public async Task<ContratoGetModel?> GetContratoAdmin(int contratoId)
        {
            Contrato? contrato = await _contratoRepository.GetContratoAdmin(contratoId);
            return contrato != null ? 
                _contratoMapper.ToGetModel(contrato) : 
                throw new NotFoundException();
        }

        public async Task<IReadOnlyCollection<ContratoGetModel>> GetContratos()
        {
            IReadOnlyCollection<Contrato> contratos = await _contratoRepository.GetContratosAdmin();
            return contratos.Select(c => _contratoMapper.ToGetModel(c)).ToList();
        }
    }
}
