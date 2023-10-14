namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public interface IPublicacionMapper : IGetMapper<Publicacion, PublicacionGetModel>,
        IPostMapper<Publicacion, PublicacionPostModel>,
        IPutMapper<Publicacion, PublicacionCancelPutModel>,
        IPutMapper<Postulacion, PublicacionCancelPutModel>
    {

    }
}
