namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public interface IUnidadMapper :
        IGetMapper<Unidad, UnidadGetModel>,
        IPostMapper<Unidad, UnidadPostModel>,
        IPutMapper<Unidad, UnidadPutModel>,
        IPostMapper<UnidadDetalle, UnidadDetalleModel>,
        IPutMapper<UnidadDetalle, UnidadDetalleModel>,
        IPostMapper<UnidadImagen, UnidadImagenModel>,
        IPutMapper<UnidadImagen, UnidadImagenModel>
    {

    }
}
