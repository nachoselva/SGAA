namespace SGAA.Service
{
    using SGAA.Domain.Core;
    using SGAA.Domain.Errors;
    using SGAA.Models;
    using SGAA.Models.Mappers;
    using SGAA.Repository.Contracts;
    using SGAA.Service.Contracts;
    using System.Threading.Tasks;

    public class UnidadService : IUnidadService
    {
        private readonly IUnidadRepository _unidadRepository;
        private readonly IUnidadMapper _unidadMapper;

        public UnidadService(IUnidadRepository unidadRepository, IUnidadMapper unidadMapper)
        {
            _unidadRepository = unidadRepository;
            _unidadMapper = unidadMapper;
        }

        public async Task<UnidadGetModel> AddUnidad(UnidadPostModel model)
        {
            Propiedad? propiedad = await _unidadRepository.GetPropiedadByDireccion(model.CiudadId, model.Calle, model.Altura);
            if (propiedad != null)
                model.PropiedadId = propiedad.Id;
            Unidad unidad = model.ToEntity(_unidadMapper);
            unidad = await _unidadRepository.AddUnidad(unidad);
            return unidad.MapToGetModel(_unidadMapper);
        }

        public async Task<UnidadGetModel> GetUnidad(int unidadId)
        {
            Unidad? unidad = await _unidadRepository.GetUnidadById(unidadId);
            if (unidad == null)
                throw new NotFoundException();
            return unidad.MapToGetModel(_unidadMapper);
        }
    }
}
