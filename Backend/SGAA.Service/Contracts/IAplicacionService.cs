namespace SGAA.Service.Contracts
{
    using SGAA.Models;
    using System.Threading.Tasks;

    public interface IAplicacionService
    {
        Task<AplicacionGetModel?> GetActiveAplicacion(int inquilinoUsuarioId);
        Task<AplicacionGetModel> AddAplicacion(AplicacionPostModel model);
        Task<AplicacionGetModel> UpdateActiveAplicacion(AplicacionPutModel model);
    }
}
