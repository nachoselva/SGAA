namespace SGAA.Service.Contracts
{
    using SGAA.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPublicacionService
    {
        Task<PublicacionGetModel> AddPublicacion(PublicacionPostModel model);
        Task<PublicacionGetModel> CancelPublicacion(int publicacionId, PublicacionCancelPutModel model);
        Task<PublicacionGetModel> GetPublicacion(int publicacionId);
        Task<IReadOnlyCollection<PublicacionGetModel>> GetPublicacionesAdmin();
    }
}
