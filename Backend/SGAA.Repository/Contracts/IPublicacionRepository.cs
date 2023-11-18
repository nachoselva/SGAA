namespace SGAA.Repository.Contracts
{
    using SGAA.Domain.Core;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPublicacionRepository
    {
        Task<Publicacion?> GetPublicacion(int publicacionId);
        Task<Publicacion?> GetPublicacion(string codigo);
        Task<IReadOnlyCollection<Publicacion>> GetPublicaciones();
        Task<Publicacion> AddPublicacion(Publicacion publicacion);
        Task<Publicacion> UpdatePublicacion(Publicacion publicacion);
        Task<IReadOnlyCollection<Publicacion>> GetPublicacionesByPropietario(int propietarioUsuarioId);
    }
}
