namespace SGAA.Service
{
    using SGAA.Domain.Core;
    using SGAA.Domain.Errors;
    using SGAA.Models;
    using SGAA.Models.Mappers;
    using SGAA.Repository.Contracts;
    using SGAA.Service.Contracts;
    using System.Collections.Generic;
    using System.Linq;
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

        public async Task<IReadOnlyCollection<UnidadGetModel>> GetUnidades(int propietarioUserId)
        {
            IReadOnlyCollection<Unidad> unidades = await _unidadRepository.GetUnidadesByPropietario(propietarioUserId);
            return unidades.Select(unidad => unidad.MapToGetModel(_unidadMapper)).ToList();
        }

        public async Task<IReadOnlyCollection<UnidadGetModel>> GetUnidadesAdmin()
        {
            IReadOnlyCollection<Unidad> unidades = await _unidadRepository.GetUnidades();
            return unidades.Select(unidad => unidad.MapToGetModel(_unidadMapper)).ToList();
        }

        public async Task<UnidadGetModel> GetUnidad(int unidadId)
        {
            Unidad? unidad = await _unidadRepository.GetUnidadById(unidadId);
            return unidad != null ? unidad.MapToGetModel(_unidadMapper) : throw new NotFoundException();
        }

        public async Task<UnidadGetModel> AddUnidad(UnidadPostModel model)
        {
            Unidad? unidadExistente = await _unidadRepository.GetUnidadByDireccion(model.CiudadId, model.Calle, model.Altura, model.Piso, model.Departamento);
            if (unidadExistente != null)
                throw new BadRequestException("Unidad", "Existe una unidad registrada en el mismo domicilio.");
            Propiedad? propiedad = await _unidadRepository.GetPropiedadByDireccion(model.CiudadId, model.Calle, model.Altura);
            if (propiedad != null)
                model.PropiedadId = propiedad.Id;
            Unidad unidad = model.ToEntity<Unidad, UnidadPostModel>(_unidadMapper);
            unidad.Detalle = model.Detalle.ToEntity<UnidadDetalle, UnidadDetalleModel>(_unidadMapper);
            unidad.Detalle.AddImagenes(model.Detalle.Imagenes!.Select(newmodel => newmodel.ToEntity<UnidadImagen, UnidadImagenModel>(_unidadMapper)));
            unidad = await _unidadRepository.AddUnidad(unidad);
            return unidad.MapToGetModel(_unidadMapper);
        }

        public async Task<UnidadGetModel> UpdateUnidad(int unidadId, UnidadPutModel putModel)
        {
            Unidad? unidad = await _unidadRepository.GetUnidadById(unidadId) ?? throw new NotFoundException();
            if (unidad.Status != UnidadStatus.AprobacionPendiente)
                throw new BadRequestException(nameof(unidad.Status), "La unidad no se encuentrá en estado editable");
            Propiedad? propiedad = await _unidadRepository.GetPropiedadByDireccion(putModel.CiudadId, putModel.Calle, putModel.Altura);
            if (propiedad != null)
                putModel.PropiedadId = propiedad.Id;

            unidad = putModel.ToEntity(_unidadMapper, unidad);
            unidad.Detalle = putModel.Detalle.ToEntity(_unidadMapper, unidad.Detalle);

            UnidadImagenModel[] updatedImageModels = putModel.Detalle.Imagenes!.Where(img => img.Id.HasValue && img.Id.Value > 0).ToArray();
            UnidadImagenModel[] newImageModels = putModel.Detalle.Imagenes!.Except(updatedImageModels).ToArray();
            int[] notDeletedIds = updatedImageModels.Where(img => img.Id.HasValue).Select(img => img.Id!.Value).ToArray();

            //Add imagenes

            unidad.Detalle.AddImagenes(newImageModels.Select(newmodel => newmodel.ToEntity<UnidadImagen, UnidadImagenModel>(_unidadMapper)));

            //Update imagenes

            foreach (var updateImageModel in updatedImageModels)
            {
                UnidadImagen imagen = unidad.Detalle.Imagenes.First(img => img.Id == updateImageModel.Id);
                imagen = updateImageModel.ToEntity(_unidadMapper, imagen);
            }

            //Delete imagenes

            UnidadImagen[] entitiesToDelete = unidad.Detalle.Imagenes
                .Where(entity => entity.Id > 0)
                .Where(entity => !notDeletedIds.Contains(entity.Id))
                .ToArray();

            unidad.Detalle.RemoveImagenes(entitiesToDelete);

            if (entitiesToDelete.Any())
                await _unidadRepository.DeleteImagenes(entitiesToDelete);

            unidad = await _unidadRepository.UpdateUnidad(unidad);
            return unidad.MapToGetModel(_unidadMapper);
        }
    }
}
