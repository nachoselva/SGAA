namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public interface IPublicacionMapper : IGetMapper<Publicacion, PublicacionGetModel>,
        IPostMapper<Publicacion, PublicacionPostModel>,
        IPutMapper<Publicacion, PublicacionCancelarPutModel>,
        IPutMapper<Publicacion, PublicacionCerrarPutModel>,
        IPutMapper<Postulacion, PublicacionCancelarPutModel>,
        IPutMapper<Postulacion, PublicacionCerrarPutModel>
    {

    }
}
