namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;

    public class PostulacionMapper : IPostulacionMapper
    {
        public Postulacion ToEntity(PostulacionPostModel postModel)
        => new Postulacion(postModel.PublicacionId, postModel.AplicacionId!.Value, null, PostulacionStatus.Postulada, null);

        public PostulacionGetModel ToGetModel(Postulacion entity)
        {
            throw new NotImplementedException();
        }
    }
}
