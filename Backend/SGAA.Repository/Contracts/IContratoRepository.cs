namespace SGAA.Repository.Contracts
{
    using SGAA.Domain.Core;

    public interface IContratoRepository
    {
        Task<Contrato?> GetContrato(int contratoId);
        Task<IReadOnlyCollection<Contrato>> GetContratos(int usuarioId);
        Task<IReadOnlyCollection<Contrato>> GetContratos();
        Task<Contrato> AddContrato(Contrato contrato);
        Task<Contrato> UpdateContrato(Contrato contrato);
    }
}
