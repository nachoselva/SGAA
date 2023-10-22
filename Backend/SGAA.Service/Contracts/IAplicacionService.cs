namespace SGAA.Service.Contracts
{
    using SGAA.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAplicacionService
    {
        Task<AplicacionGetModel> GetActiveAplicacion(int inquilinoUsuarioId);
        Task<AplicacionGetModel> GetAplicacion(int aplicacionId);
        Task<IReadOnlyCollection<AplicacionGetModel>> GetAplicaciones(int inquilinoUsuarioId);
        Task<IReadOnlyCollection<AplicacionGetModel>> GetAplicaciones();
        Task<AplicacionGetModel> AprobarAplicacion(int aplicacionId, AprobarAplicacionPutModel model);
        Task<AplicacionGetModel> RechazarAplicacion(int aplicacionId, RechazarAplicacionPutModel model);
        Task<AplicacionGetModel> AddAplicacion(AplicacionPostModel model);
        Task<AplicacionGetModel> UpdateActiveAplicacion(AplicacionPutModel model);
    }
}
