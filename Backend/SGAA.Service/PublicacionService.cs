namespace SGAA.Service
{
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
    using SGAA.Utils;
    using SGAA.Utils.Configuration;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PublicacionService : IPublicacionService
    {
        private readonly ISGAAConfiguration _configuration;
        private readonly IPublicacionRepository _publicacionRepository;
        private readonly IPublicacionMapper _publicacionMapper;
        private readonly IUnidadRepository _unidadRepository;
        private readonly IPublicarUnidadEmailSender _publicarUnidadEmailSender;
        private readonly ICancelarPostulacionEmailSender _cancelarPostulacionEmailSender;
        private readonly IReservaOfrecidaInquilinoEmailSender _reservaOfrecidaInquilinoEmailSender;
        private readonly IReservaOfrecidaPropietarioEmailSender _reservaOfrecidaPropietarioEmailSender;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAplicacionRepository _aplicacionRepository;

        public PublicacionService(ISGAAConfiguration configuration, IPublicacionRepository publicacionRepository,
            IPublicacionMapper publicacionMapper, IUnidadRepository unidadRepository, IPublicarUnidadEmailSender publicarUnidadEmailSender,
            ICancelarPostulacionEmailSender cancelarPostulacionEmailSender, IReservaOfrecidaInquilinoEmailSender reservaOfrecidaInquilinoEmailSender,
            IReservaOfrecidaPropietarioEmailSender reservaOfrecidaPropietarioEmailSender, IUsuarioRepository usuarioRepository,
            IAplicacionRepository aplicacionRepository)
        {
            _configuration = configuration;
            _publicacionRepository = publicacionRepository;
            _publicacionMapper = publicacionMapper;
            _unidadRepository = unidadRepository;
            _publicarUnidadEmailSender = publicarUnidadEmailSender;
            _cancelarPostulacionEmailSender = cancelarPostulacionEmailSender;
            _reservaOfrecidaInquilinoEmailSender = reservaOfrecidaInquilinoEmailSender;
            _reservaOfrecidaPropietarioEmailSender = reservaOfrecidaPropietarioEmailSender;
            _usuarioRepository = usuarioRepository;
            _aplicacionRepository = aplicacionRepository;
        }
        private bool CanUsuarioPostular(Usuario usuario, Publicacion publicacion)
        {
            return usuario.UsuarioRoles.Any(ur => ur.Rol.RolType == RolType.Inquilino)
                && publicacion.Status == PublicacionStatus.Publicada;
        }

        public async Task<PublicacionGetModel> GetActivePublicacion(int? usuarioId, string codigo)
        {
            Publicacion? publicacion = await _publicacionRepository.GetPublicacion(codigo);
            PublicacionGetModel model = (publicacion != null && publicacion.Status.IsActive() ?
                publicacion.MapToGetModel(_publicacionMapper) :
                throw new NotFoundException());
            Usuario? usuario = usuarioId.HasValue ? await _usuarioRepository.GetUsuarioById(usuarioId.Value) : null;
            if (usuario != null)
                model.CanUsuarioPostular = CanUsuarioPostular(usuario, publicacion);
            return model;
        }

        public async Task<PublicacionGetModel> GetPublicacion(int propietarioUsuarioId, int publicacionId)
        {
            Publicacion? publicacion = await _publicacionRepository.GetPublicacion(publicacionId);
            return publicacion != null && publicacion.Unidad.PropietarioUsuarioId == propietarioUsuarioId
                ? publicacion.MapToGetModel(_publicacionMapper) : throw new NotFoundException();
        }

        public async Task<PublicacionGetModel> GetPublicacion(int publicacionId)
        {
            Publicacion? publicacion = await _publicacionRepository.GetPublicacion(publicacionId);
            return publicacion != null ? publicacion.MapToGetModel(_publicacionMapper) : throw new NotFoundException();
        }

        public async Task<PublicacionGetModel> GetPublicacionByInquilino(int inquilinoUsuarioId, int publicacionId)
        {
            Publicacion? publicacion = await _publicacionRepository.GetPublicacion(publicacionId);
            return publicacion != null &&
                publicacion.Postulaciones.Any(p => p.Aplicacion.InquilinoUsuarioId == inquilinoUsuarioId)
                ? publicacion.MapToGetModel(_publicacionMapper) : throw new NotFoundException();
        }

        public async Task<IReadOnlyCollection<PublicacionGetModel>> GetActivePublicaciones(int? usuarioId)
        {
            IReadOnlyCollection<Publicacion> publicaciones = await _publicacionRepository.GetPublicaciones();

            Usuario? usuario = usuarioId.HasValue ? await _usuarioRepository.GetUsuarioById(usuarioId.Value) : null;
            Aplicacion? activeAplicacion = null;
            if (usuario != null)
            {
                IReadOnlyCollection<Aplicacion> aplicaciones = await _aplicacionRepository
                    .GetAplicaciones(usuario.Id);

                activeAplicacion = aplicaciones
                    .FirstOrDefault(ap => ap.Status == AplicacionStatus.Aprobada);
            }

            return publicaciones.Where(p => p.Status.IsActive())
                .Select(p =>
                {
                    var model = p.MapToGetModel(_publicacionMapper);
                    if (usuario != null && activeAplicacion != null)
                        model.CanUsuarioPostular = CanUsuarioPostular(usuario, p);
                    return model;
                }).ToList();
        }

        public async Task<IReadOnlyCollection<PublicacionGetModel>> GetPublicaciones(int propietarioUsuarioId)
        {
            IReadOnlyCollection<Publicacion> publicaciones = await _publicacionRepository.GetPublicacionesByPropietario(propietarioUsuarioId);
            return publicaciones.Select(publicacion => publicacion.MapToGetModel(_publicacionMapper)).ToList();
        }

        public async Task<IReadOnlyCollection<PublicacionGetModel>> GetPublicaciones()
        {
            IReadOnlyCollection<Publicacion> publicaciones = await _publicacionRepository.GetPublicaciones();
            return publicaciones.Select(publicacion => publicacion.MapToGetModel(_publicacionMapper)).ToList();
        }

        public async Task<PublicacionGetModel> AddPublicacion(PublicacionPostModel model)
        {
            Unidad? unidad = await _unidadRepository.GetUnidad(model.UnidadId);
            if (unidad == null || model.PropietarioUsuarioId != unidad.PropietarioUsuarioId)
                throw new NotFoundException();
            if (unidad.Status != UnidadStatus.DocumentacionAprobada)
                throw new BadRequestException(nameof(unidad.Status), "La unidad no tiene su documentación aprobada");
            if (unidad.Publicaciones.Any(p => p.Status.IsActive()))
                throw new BadRequestException(nameof(Publicacion.Status), "La unidad ya tiene una publicación activa");
            model.Codigo = StringExtensions.GenerateRandomString(30);
            Publicacion publicacion = model.ToEntity(_publicacionMapper);
            publicacion = await _publicacionRepository.AddPublicacion(publicacion);

            string publicacionURL = $"{_configuration.Frontend.Url}/publicacion/{publicacion.Codigo}";

            await _publicarUnidadEmailSender.SendEmail(unidad.PropietarioUsuario.Email!,
                 new PublicarUnidadEmailModel
                 {
                     Nombre = unidad.PropietarioUsuario.Nombre,
                     Apellido = unidad.PropietarioUsuario.Apellido,
                     Domicilio = unidad.DomicilioCompleto,
                     InicioAlquiler = publicacion.InicioAlquiler.ToShortDateString(),
                     MontoAlquiler = publicacion.MontoAlquiler,
                     PublicacionURL = publicacionURL
                 });

            return publicacion.MapToGetModel(_publicacionMapper);
        }

        public async Task<PublicacionGetModel> CancelPublicacion(int publicacionId, PublicacionCancelarPutModel model)
        {
            Publicacion? publicacion = await _publicacionRepository.GetPublicacion(publicacionId);
            if (publicacion == null || model.PropietarioUsuarioId != publicacion.Unidad.PropietarioUsuarioId)
                throw new NotFoundException();
            if (publicacion.Status != PublicacionStatus.Publicada)
                throw new BadRequestException(nameof(publicacion.Status), "La publicación no se encuentra en estado para cancelar");
            publicacion = model.ToEntity(_publicacionMapper, publicacion);
            foreach (var postulacion in publicacion.Postulaciones.Where(p => p.Status == PostulacionStatus.Postulada))
            {
                model.ToEntity(_publicacionMapper, postulacion);
            }
            List<Usuario> usuariosToBeNotified = new();
            foreach (var postulacion in publicacion.Postulaciones
                .Where(p => p.Status == PostulacionStatus.Postulada && p.Aplicacion.Status == AplicacionStatus.Aprobada))
            {
                postulacion.Status = PostulacionStatus.PublicacionCancelada;
                usuariosToBeNotified.Add(postulacion.Aplicacion.InquilinoUsuario);
            }
            publicacion = await _publicacionRepository.UpdatePublicacion(publicacion);
            foreach (var usuario in usuariosToBeNotified)
            {
                await _cancelarPostulacionEmailSender.SendEmail(usuario.Email!,
                    new CancelarPostulacionEmailModel()
                    {
                        Nombre = usuario.Nombre,
                        Apellido = usuario.Apellido,
                        Domicilio = publicacion.Unidad.DomicilioCompleto,
                        IsPropietarioAction = true
                    });
            }
            return publicacion.MapToGetModel(_publicacionMapper);
        }

        public async Task<PublicacionGetModel> CerrarPublicacion(int publicacionId, PublicacionCerrarPutModel model)
        {
            Publicacion? publicacion = await _publicacionRepository.GetPublicacion(publicacionId);
            if (publicacion == null || model.PropietarioUsuarioId != publicacion.Unidad.PropietarioUsuarioId)
                throw new NotFoundException();
            if (publicacion.Status != PublicacionStatus.Publicada)
                throw new BadRequestException(nameof(publicacion.Status), "La publicación no se encuentra en estado para cerrar");
            var postulaciones = publicacion.Postulaciones
                .Where(p => p.Status == PostulacionStatus.Postulada)
                .Where(p => p.Aplicacion.Status == AplicacionStatus.Aprobada);
            var postulacionSelected = postulaciones
                .OrderByDescending(p => p.Aplicacion.PuntuacionTotal)
                .FirstOrDefault()
                ?? throw new BadRequestException(nameof(publicacion.Postulaciones), "La publicación no tiene postulaciones");
            publicacion = model.ToEntity(_publicacionMapper, publicacion);
            postulacionSelected = model.ToEntity(_publicacionMapper, postulacionSelected);
            publicacion = await _publicacionRepository.UpdatePublicacion(publicacion);

            Usuario propietario = publicacion.Unidad.PropietarioUsuario;
            Usuario inquilino = postulacionSelected.Aplicacion.InquilinoUsuario;

            await _reservaOfrecidaPropietarioEmailSender.SendEmail(propietario.Email!,
                   new ReservaOfrecidaPropietarioEmailModel()
                   {
                       Nombre = propietario.Nombre,
                       Apellido = propietario.Apellido,
                       Domicilio = publicacion.Unidad.DomicilioCompleto
                   });

            await _reservaOfrecidaInquilinoEmailSender.SendEmail(inquilino.Email!,
                    new ReservaOfrecidaInquilinoEmailModel()
                    {
                        Nombre = inquilino.Nombre,
                        Apellido = inquilino.Apellido,
                        Domicilio = publicacion.Unidad.DomicilioCompleto
                    });

            return publicacion.MapToGetModel(_publicacionMapper);
        }
    }
}
