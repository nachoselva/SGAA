namespace SGAA.Repository
{
    using SGAA.Domain.Core;

    public interface IProvinciaRepository
    {
        Task<IReadOnlyCollection<Provincia>> GetAllProvincias();
    }
}
