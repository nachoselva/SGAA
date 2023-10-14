namespace SGAA.Service
{
    using SGAA.Domain.Core;
    using SGAA.Domain.Errors;
    using SGAA.Models;
    using SGAA.Models.Mappers;
    using SGAA.Repository.Contracts;
    using SGAA.Service.Contracts;
    using SGAA.Utils;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PublicacionService : IPublicacionService
    {
        private readonly IPublicacionRepository _publicacionRepository;
        private readonly IPublicacionMapper _publicacionMapper;
        private readonly IUnidadRepository _unidadRepository;

        public PublicacionService(IPublicacionRepository publicacionRepository, IPublicacionMapper publicacionMapper, IUnidadRepository unidadRepository)
        {
            _publicacionRepository = publicacionRepository;
            _publicacionMapper = publicacionMapper;
            _unidadRepository = unidadRepository;
        }

        public async Task<PublicacionGetModel> AddPublicacion(PublicacionPostModel model)
        {
            Unidad? unidad = await _unidadRepository.GetUnidadById(model.UnidadId);
            if (unidad == null || model.PropietarioUsuarioId != unidad.PropietarioUsuarioId)
                throw new NotFoundException();
            if (unidad.Status != UnidadStatus.AprobacionPendiente)
                throw new BadRequestException(nameof(unidad.Status), "La unidad no tiene su documentación aprobada");
            if (unidad.Publicaciones.Any(p => p.Status.IsActive()))
                throw new BadRequestException(nameof(Publicacion.Status), "La unidad ya tiene una publicación activa");
            model.Codigo = StringExtensions.GenerateRandomString(30);
            Publicacion publicacion = model.ToEntity(_publicacionMapper);
            publicacion = await _publicacionRepository.AddPublicacion(publicacion);
            return publicacion.MapToGetModel(_publicacionMapper);
        }

        public async Task<PublicacionGetModel> CancelPublicacion(int publicacionId, PublicacionCancelPutModel model)
        {
            Publicacion? publicacion = await _publicacionRepository.GetPublicacionById(publicacionId);
            if (publicacion == null || model.PropietarioUsuarioId != publicacion.Unidad.PropietarioUsuarioId)
                throw new NotFoundException();
            if (publicacion.Status != PublicacionStatus.Publicada)
                throw new BadRequestException(nameof(publicacion.Status), "La publicación no se encuentra en estado para cancelar");
            publicacion = model.ToEntity(_publicacionMapper, publicacion);
            foreach (var postulacion in publicacion.Postulaciones.Where(p => p.Status == PostulacionStatus.Postulada))
            {
                model.ToEntity(_publicacionMapper, postulacion);
            }
            publicacion = await _publicacionRepository.UpdatePublicacion(publicacion);
            return publicacion.MapToGetModel(_publicacionMapper);
        }

        public async Task<PublicacionGetModel> GetPublicacionActiveByCodigo(string codigo)
        {
            Publicacion? publicacion = await _publicacionRepository.GetPublicacionByCodigo(codigo);
            return publicacion != null && publicacion.Status.IsActive() ?
                publicacion.MapToGetModel(_publicacionMapper) :
                throw new NotFoundException();
        }

        public async Task<PublicacionGetModel> GetPublicacionByPublicacionId(int publicacionId)
        {
            Publicacion? publicacion = await _publicacionRepository.GetPublicacionById(publicacionId);
            return publicacion != null ? publicacion.MapToGetModel(_publicacionMapper) : throw new NotFoundException();
        }

        public async Task<IReadOnlyCollection<PublicacionGetModel>> GetPublicacionesAdmin()
        {
            IReadOnlyCollection<Publicacion> publicaciones = await _publicacionRepository.GetPublicaciones();
            return publicaciones.Select(publicacion => publicacion.MapToGetModel(_publicacionMapper)).ToList();
        }
    }
}
