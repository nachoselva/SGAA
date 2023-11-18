namespace SGAA.Service.Contracts
{
    using SGAA.Models;
    using System.Collections.Generic;

    public interface IUnidadService
    {
        Task<IReadOnlyCollection<UnidadGetModel>> GetUnidades(int propietarioUsuarioId);
        Task<IReadOnlyCollection<UnidadGetModel>> GetUnidades();
        Task<UnidadGetModel> GetUnidad(int propietarioUsuarioId, int unidadId);
        Task<UnidadGetModel> GetUnidad(int unidadId);
        Task<UnidadGetModel> AddUnidad(UnidadPostModel model);
        Task<UnidadGetModel> UpdateUnidad(int unidadId, UnidadPutModel model);
        Task<UnidadGetModel> AprobarUnidad(int unidadId, AprobarUnidadPutModel model);
        Task<UnidadGetModel> RechazarUnidad(int unidadId, RechazarUnidadPutModel model);
    }
}
