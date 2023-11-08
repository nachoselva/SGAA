namespace SGAA.Service.Contracts
{
    using SGAA.Models;
    using System.Threading.Tasks;

    public interface IPagoService
    {
        Task<PagoGetModel> GetPagoByPropietario(int propietarioUsuarioId, int pagoId);
        Task<IReadOnlyCollection<PagoGetModel>> GetPagosByPropietario(int propietarioUsuarioId);
        Task<IReadOnlyCollection<PagoGetModel>> GetPagosByPropietarioAndContrato(int propietarioUsuarioId, int contratoId);
        Task<PagoGetModel> GetPagoByInquilino(int inquilinoUsuarioId, int pagoId);
        Task<IReadOnlyCollection<PagoGetModel>> GetPagosByInquilino(int inquilinoUsuarioId);
        Task<IReadOnlyCollection<PagoGetModel>> GetPagosByInquilinoAndContrato(int inquilinoUsuarioId, int contratoId);
        Task<PagoGetModel> GetPago(int pagoId);
        Task<IReadOnlyCollection<PagoGetModel>> GetPagos();
        Task<IReadOnlyCollection<PagoGetModel>> CreatePagosContrato(int contratoId);
        Task<PagoGetModel> AddPago(PagoPostModel model);
        Task<PagoGetModel> AbonarPago(int pagoId, AbonarPagoPutModel model);
        Task<PagoGetModel> AprobarPago(int pagoId, AprobarPagoPutModel model);
    }
}
