namespace SGAA.Service.Contracts
{
    using SGAA.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IContratoService
    {
        Task<ContratoGetModel> CreateContrato(int postulacionId, DateOnly fechaDesde, DateOnly fechaHasta);
        Task<ContratoGetModel?> GetContratoAdmin(int contratoId);
        Task<IReadOnlyCollection<ContratoGetModel>> GetContratos();
    }
}
