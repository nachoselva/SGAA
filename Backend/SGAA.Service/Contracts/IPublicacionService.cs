namespace SGAA.Service.Contracts
{
    using SGAA.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPublicacionService
    {
        Task<IReadOnlyCollection<PublicacionGetModel>> GetPublicaciones(int propietarioUsuarioId);
        Task<IReadOnlyCollection<PublicacionGetModel>> GetPublicaciones();
        Task<IReadOnlyCollection<PublicacionGetModel>> GetActivePublicaciones(int? usuarioId);
        Task<PublicacionGetModel> GetPublicacion(int propietarioUsuarioId, int publicacionId);
        Task<PublicacionGetModel> GetPublicacionByInquilino(int inquilinoUsuarioId, int publicacionId);
        Task<PublicacionGetModel> GetPublicacion(int publicacionId);
        Task<PublicacionGetModel> GetActivePublicacion(int? usuarioId, string codigo);
        Task<PublicacionGetModel> AddPublicacion(PublicacionPostModel model);
        Task<PublicacionGetModel> CancelPublicacion(int publicacionId, PublicacionCancelarPutModel model);
        Task<PublicacionGetModel> CerrarPublicacion(int publicacionId, PublicacionCerrarPutModel model);
    }
}
