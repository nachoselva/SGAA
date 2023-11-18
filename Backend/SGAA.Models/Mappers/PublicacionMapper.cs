namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;

    public class PublicacionMapper : IPublicacionMapper
    {
        private readonly IUnidadMapper _unidadMapper;

        public PublicacionMapper(IUnidadMapper unidadMapper)
        {
            _unidadMapper = unidadMapper;
        }

        public Publicacion ToEntity(PublicacionPostModel postModel)
        => new(
            postModel.UnidadId,
            postModel.MontoAlquiler,
            new DateOnly(postModel.InicioAlquiler.Year, postModel.InicioAlquiler.Month, postModel.InicioAlquiler.Day),
            postModel.Codigo!,
            PublicacionStatus.Publicada);

        public Publicacion ToEntity(PublicacionCancelarPutModel putModel, Publicacion entity)
        {
            entity.Status = PublicacionStatus.Cancelada;
            return entity;
        }

        public Postulacion ToEntity(PublicacionCancelarPutModel putModel, Postulacion entity)
        {
            entity.Status = PostulacionStatus.PublicacionCancelada;
            return entity;
        }

        public Publicacion ToEntity(PublicacionCerrarPutModel putModel, Publicacion entity)
        {
            entity.Status = PublicacionStatus.Ofrecida;
            return entity;
        }

        public Postulacion ToEntity(PublicacionCerrarPutModel putModel, Postulacion entity)
        {
            entity.Status = PostulacionStatus.Ofrecida;
            entity.FechaOferta = DateTime.Now;
            return entity;
        }

        public PublicacionGetModel ToGetModel(Publicacion entity)
        => new()
        {
            Id = entity.Id,
            UnidadId = entity.UnidadId,
            Status = entity.Status,
            Codigo = entity.Codigo,
            InicioAlquiler = entity.InicioAlquiler,
            MontoAlquiler = entity.MontoAlquiler,
            DomicilioCompleto = entity.Unidad.DomicilioCompleto,
            Postulaciones = entity.Postulaciones?.Where(p => p.Status.IsActive()).Count(),
            Unidad = entity.Unidad.MapToGetModel<Unidad, UnidadGetModel>(_unidadMapper)
        };
    }
}
