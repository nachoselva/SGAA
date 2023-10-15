namespace SGAA.Service.Contracts
{
    using SGAA.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPublicacionService
    {
        Task<IReadOnlyCollection<PublicacionGetModel>> GetPublicacionesAdmin();
        Task<PublicacionGetModel> GetPublicacionActiveByCodigo(string codigo);
        Task<PublicacionGetModel> GetPublicacionByPublicacionId(int publicacionId);
        Task<PublicacionGetModel> AddPublicacion(PublicacionPostModel model);
        Task<PublicacionGetModel> CancelPublicacion(int publicacionId, PublicacionCancelarPutModel model);
        Task<PublicacionGetModel> CerrarPublicacion(int publicacionId, PublicacionCerrarPutModel model);
    }
}
