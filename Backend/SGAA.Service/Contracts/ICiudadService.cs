namespace SGAA.Service.Contracts
{
    using SGAA.Models;

    public interface ICiudadService
    {
        public Task<IReadOnlyCollection<CiudadGetModel>> GetCiudades(int provinciaId);
    }
}
