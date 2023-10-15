namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public interface IAplicacionMapper :
        IGetMapper<Aplicacion, AplicacionGetModel>,
        IPostMapper<Aplicacion, AplicacionPostModel>,
        IPutMapper<Aplicacion, AplicacionPutModel>,
        IGetMapper<Postulante, PostulanteModel>,
        IPostMapper<Postulante, PostulanteModel>,
        IPutMapper<Postulante, PostulanteModel>,
        IGetMapper<Garantia, GarantiaModel>,
        IPostMapper<Garantia, GarantiaModel>,
        IPutMapper<Garantia, GarantiaModel>,
        IGetMapper<AplicacionComentario, ComentarioModel>,
        IPutMapper<Aplicacion, RechazarAplicacionPutModel>,
        IPutMapper<Aplicacion, AprobarAplicacionPutModel>
    {

    }
}
