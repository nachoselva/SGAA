namespace SGAA.Repository.Contracts
{
    using SGAA.Domain.Core;

    public interface IAplicacionRepository
    {
        Task<IReadOnlyCollection<Aplicacion>> GetAplicacionesByInquilinoUsuarioId(int usuarioId);
    }
}
