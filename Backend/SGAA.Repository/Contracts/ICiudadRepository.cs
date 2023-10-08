namespace SGAA.Repository.Contracts
{
    using SGAA.Domain.Core;

    public interface ICiudadRepository
    {
        Task<IReadOnlyCollection<Ciudad>> GetAllCiudades(int provinciaId);
    }
}
