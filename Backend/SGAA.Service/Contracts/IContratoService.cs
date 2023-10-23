namespace SGAA.Service.Contracts
{
    using SGAA.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IContratoService
    {
        Task<ContratoGetModel> GetContrato(int usuarioId, int contratoId);
        Task<ContratoGetModel> GetContrato(int contratoId);
        Task<IReadOnlyCollection<ContratoGetModel>> GetContratos(int usuarioId);
        Task<IReadOnlyCollection<ContratoGetModel>> GetContratos();
        Task<ContratoGetModel> CreateContrato(int postulacionId, DateOnly fechaDesde, DateOnly fechaHasta);
        Task<ContratoGetModel> FirmarContrato(int usuarioId, int contratoId, string direccionIp);
        Task<ContratoGetModel> RenovarContrato(int contratoId, RenovarContratoPostModel model);
        Task<ContratoGetModel> CancelarContrato(int contratoId);
    }
}
