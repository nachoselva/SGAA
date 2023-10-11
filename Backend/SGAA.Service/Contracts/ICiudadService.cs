namespace SGAA.Service.Contracts
{
    using SGAA.Models;

    public interface ICiudadService
    {
        Task<IReadOnlyCollection<CiudadGetModel>> GetCiudades(int provinciaId);
    }
}
