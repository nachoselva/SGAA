namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public interface IUnidadMapper :
        IGetMapper<Unidad, UnidadGetModel>,
        IPostMapper<Unidad, UnidadPostModel>,
        IPutMapper<Unidad, UnidadPutModel>,
        IPutMapper<Unidad, AprobarUnidadPutModel>,
        IPutMapper<Unidad, RechazarUnidadPutModel>,
        IPostMapper<UnidadDetalle, UnidadDetalleModel>,
        IPutMapper<UnidadDetalle, UnidadDetalleModel>,
        IPostMapper<UnidadImagen, UnidadImagenModel>,
        IPutMapper<UnidadImagen, UnidadImagenModel>,
        IGetMapper<Titular, TitularModel>,
        IPutMapper<Titular, TitularModel>,
        IPostMapper<Titular, TitularModel>,
        IGetMapper<UnidadDetalle, UnidadDetalleModel>,
        IGetMapper<UnidadComentario, ComentarioModel>,
        IGetMapper<UnidadImagen, UnidadImagenModel>
    {

    }
}
