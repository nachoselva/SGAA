namespace SGAA.Repository.Contracts
{
    using SGAA.Domain.Core;

    public interface IContratoRepository
    {
        Task<Contrato> AddContrato(Contrato contrato);
        Task<Contrato?> GetContrato(int contratoId);
        Task<IReadOnlyCollection<Contrato>> GetContratosAdmin();
        Task<IReadOnlyCollection<Contrato>> GetContratosByUsuarioId(int usuarioId);
        Task<Contrato> UpdateContrato(Contrato contrato);
    }
}
