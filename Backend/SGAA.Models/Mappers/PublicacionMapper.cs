namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;

    public class PublicacionMapper : IPublicacionMapper
    {
        public Publicacion ToEntity(PublicacionPostModel postModel)
        => new(
            postModel.UnidadId,
            postModel.MontoAlquiler,
            postModel.InicioAlquiler,
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
            MontoAlquiler = entity.MontoAlquiler
        };
    }
}
