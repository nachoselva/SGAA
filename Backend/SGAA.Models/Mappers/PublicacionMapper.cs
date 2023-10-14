namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;

    public class PublicacionMapper : IPublicacionMapper
    {
        public Publicacion ToEntity(PublicacionPostModel postModel)
        {
            return new(
                postModel.UnidadId,
                postModel.MontoAlquiler,
                postModel.InicioAlquiler,
                postModel.Codigo!,
                PublicacionStatus.Publicada);
        }

        public Publicacion ToEntity(PublicacionCancelPutModel putModel, Publicacion entity)
        {
            entity.Status = PublicacionStatus.Cancelada;
            return entity;
        }

        public Postulacion ToEntity(PublicacionCancelPutModel putModel, Postulacion entity)
        {
            entity.Status = PostulacionStatus.PublicacionCancelada;
            return entity;
        }

        public PublicacionGetModel ToGetModel(Publicacion entity)
        {
            return new()
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
}
