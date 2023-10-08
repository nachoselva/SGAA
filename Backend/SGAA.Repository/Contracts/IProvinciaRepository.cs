namespace SGAA.Repository.Contracts
{
    using SGAA.Domain.Core;

    public interface IProvinciaRepository
    {
        Task<IReadOnlyCollection<Provincia>> GetAllProvincias();
    }
}
