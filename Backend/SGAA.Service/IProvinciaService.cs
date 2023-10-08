namespace SGAA.Service
{
    using SGAA.Models;

    public interface IProvinciaService
    {
        public Task<IReadOnlyCollection<ProvinciaGetModel>> GetProvincias();
    }
}
