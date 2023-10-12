namespace SGAA.Service.Contracts
{
    using SGAA.Models;

    public interface IUnidadService
    {
        Task<UnidadGetModel> AddUnidad(UnidadPostModel model);
        Task<UnidadGetModel> GetUnidad(int unidadId);
    }
}
