namespace SGAA.Service
{
    using SGAA.Domain.Core;
    using SGAA.Domain.Errors;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
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
        private readonly IAprobarUnidadEmailSender _aprobarUnidadEmailSender;
        private readonly IRechazarUnidadEmailSender _rechazarUnidadEmailSender;

        public UnidadService(IUnidadRepository unidadRepository, IUnidadMapper unidadMapper, IAprobarUnidadEmailSender aprobarUnidadEmailSender,
            IRechazarUnidadEmailSender rechazarUnidadEmailSender)
        {
            _unidadRepository = unidadRepository;
            _unidadMapper = unidadMapper;
            _aprobarUnidadEmailSender = aprobarUnidadEmailSender;
            _rechazarUnidadEmailSender = rechazarUnidadEmailSender;
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
            Unidad? unidad = await _unidadRepository.GetUnidadById(unidadId);
            if (unidad == null || putModel.PropietarioUsuarioId != unidad.PropietarioUsuarioId)
                throw new NotFoundException();
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

        public async Task<UnidadGetModel> AprobarUnidad(int unidadId, AprobarUnidadPutModel model)
        {
            Unidad? unidad = await _unidadRepository.GetUnidadById(unidadId) ?? throw new NotFoundException();
            if (unidad.Status != UnidadStatus.AprobacionPendiente)
                throw new BadRequestException(nameof(unidad.Status), "La unidad no se encuentrá en estado para aprobar");
            unidad = model.ToEntity(_unidadMapper, unidad);
            unidad = await _unidadRepository.UpdateUnidad(unidad);

            await _aprobarUnidadEmailSender.SendEmail(unidad.PropietarioUsuario.Email!,
                 new AprobarUnidadEmailModel
                 {
                     Nombre = unidad.PropietarioUsuario.Nombre,
                     Apellido = unidad.PropietarioUsuario.Apellido,
                     Domicilio = unidad.DomicilioCompleto
                 });

            return unidad.MapToGetModel(_unidadMapper);
        }

        public async Task<UnidadGetModel> RechazarUnidad(int unidadId, RechazarUnidadPutModel model)
        {
            Unidad? unidad = await _unidadRepository.GetUnidadById(unidadId) ?? throw new NotFoundException();
            if (unidad.Status != UnidadStatus.AprobacionPendiente)
                throw new BadRequestException(nameof(unidad.Status), "La unidad no se encuentrá en estado para aprobar");
            unidad = model.ToEntity(_unidadMapper, unidad);
            unidad = await _unidadRepository.UpdateUnidad(unidad);

            await _rechazarUnidadEmailSender.SendEmail(unidad.PropietarioUsuario.Email!,
                 new RechazarUnidadEmailModel
                 {
                     Nombre = unidad.PropietarioUsuario.Nombre,
                     Apellido = unidad.PropietarioUsuario.Apellido,
                     Domicilio = unidad.DomicilioCompleto,
                     Comentarios = unidad.Comentarios.OrderByDescending(c => c.Fecha)
                     .Select(c =>
                     new RechazarUnidadComentarioEmailModel
                     {
                         Fecha = $"{c.Fecha.ToShortDateString()} {c.Fecha.ToShortTimeString()}",
                         Comentario = c.Comentario
                     }).ToList()
                 });

            return unidad.MapToGetModel(_unidadMapper);
        }
    }
}
