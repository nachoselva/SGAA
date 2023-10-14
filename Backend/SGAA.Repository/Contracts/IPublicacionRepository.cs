namespace SGAA.Repository.Contracts
{
    using SGAA.Domain.Core;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPublicacionRepository
    {
        Task<Publicacion?> GetPublicacionById(int publicacionId);
        Task<Publicacion?> GetPublicacionByCodigo(string codigo);
        Task<IReadOnlyCollection<Publicacion>> GetPublicaciones();
        Task<Publicacion> AddPublicacion(Publicacion publicacion);
        Task<Publicacion> UpdatePublicacion(Publicacion publicacion);
    }
}
