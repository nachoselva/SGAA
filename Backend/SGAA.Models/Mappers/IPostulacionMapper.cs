namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public interface IPostulacionMapper :
        IGetMapper<Postulacion, PostulacionGetModel>,
        IPostMapper<Postulacion, PostulacionPostModel>,
        IPutMapper<Postulacion, RechazarOfertaPostulacionPutModel>,
        IPutMapper<Publicacion, RechazarOfertaPostulacionPutModel>,
        IPutMapper<Aplicacion, RechazarOfertaPostulacionPutModel>,
        IPutMapper<Postulacion, AceptarOfertaPostulacionPutModel>,
        IPutMapper<Publicacion, AceptarOfertaPostulacionPutModel>,
        IPutMapper<Aplicacion, AceptarOfertaPostulacionPutModel>,
        IPutMapper<Postulacion, CancelarPostulacionPutModel>
    {

    }
}
