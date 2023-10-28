namespace SGAA.Service.Contracts
{
    using SGAA.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostulacionService
    {
        Task<IReadOnlyCollection<PostulacionGetModel>> GetPostulaciones(int inquilinoUsuarioId);
        Task<IReadOnlyCollection<PostulacionGetModel>> GetPostulaciones();
        Task<PostulacionGetModel> AddPostulacion(PostulacionPostModel model);
        Task<PostulacionGetModel> AceptarOferta(int postulacionId, AceptarOfertaPostulacionPutModel model);
        Task<PostulacionGetModel> RechazarOferta(int postulacionId, RechazarOfertaPostulacionPutModel model);
        Task<PostulacionGetModel> CancelarPostulacion(int postulacionId, CancelarPostulacionPutModel model);
        Task<PostulacionGetModel> GetPublicacion(int inquilinoUsuarioId, int postulacionId);
    }
}
