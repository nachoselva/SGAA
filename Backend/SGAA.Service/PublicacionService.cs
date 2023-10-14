namespace SGAA.Service
{
    using SGAA.Domain.Core;
    using SGAA.Domain.Errors;
    using SGAA.Models;
    using SGAA.Models.Mappers;
    using SGAA.Repository.Contracts;
    using SGAA.Service.Contracts;
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
                throw new BadRequestException(nameof(unidad.Status), "La unidad ya tiene una publicación activa");
            Publicacion publicacion = model.ToEntity(_publicacionMapper);
            publicacion = await _publicacionRepository.AddPublicacion(publicacion);
            return publicacion.MapToGetModel(_publicacionMapper);
        }

        public Task<PublicacionGetModel> CancelPublicacion(int publicacionId, PublicacionCancelPutModel model)
        {
            throw new NotImplementedException();
        }

        public Task<PublicacionGetModel> GetPublicacion(int publicacionId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<PublicacionGetModel>> GetPublicacionesAdmin()
        {
            throw new NotImplementedException();
        }
    }
}
