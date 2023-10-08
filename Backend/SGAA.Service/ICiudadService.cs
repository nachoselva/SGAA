namespace SGAA.Service
{
    using SGAA.Models;

    public interface ICiudadService
    {
        public Task<IReadOnlyCollection<CiudadGetModel>> GetCiudades(int provinciaId);
    }
}
