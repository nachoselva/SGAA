namespace SGAA.Service.Contracts
{
    using SGAA.Models;

    public interface IProvinciaService
    {
        public Task<IReadOnlyCollection<ProvinciaGetModel>> GetProvincias();
    }
}
