namespace SGAA.Service.Contracts
{
    using SGAA.Models;
    using System.Threading.Tasks;

    public interface IPostulacionService
    {
        Task<PostulacionGetModel> AddPostulacion(PostulacionPostModel model);
        Task<PostulacionGetModel> AceptarOferta(int postulacionId, AceptarOfertaPostulacionPutModel model);
        Task<PostulacionGetModel> RechazarOferta(int postulacionId, RechazarOfertaPostulacionPutModel model);
        Task<PostulacionGetModel> CancelarPostulacion(int postulacionId, CancelarPostulacionPutModel model);
    }
}
