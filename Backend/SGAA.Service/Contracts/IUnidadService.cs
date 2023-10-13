namespace SGAA.Service.Contracts
{
    using SGAA.Models;
    using System.Collections.Generic;

    public interface IUnidadService
    {
        Task<IReadOnlyCollection<UnidadGetModel>> GetUnidades(int propietarioUserId);
        Task<IReadOnlyCollection<UnidadGetModel>> GetUnidadesAdmin();
        Task<UnidadGetModel> GetUnidad(int unidadId);
        Task<UnidadGetModel> AddUnidad(UnidadPostModel model);
        Task<UnidadGetModel> UpdateUnidad(int unidadId, UnidadPutModel model);
    }
}
