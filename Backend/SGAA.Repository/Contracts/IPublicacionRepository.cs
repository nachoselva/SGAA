namespace SGAA.Repository.Contracts
{
    using SGAA.Domain.Core;
    using System.Threading.Tasks;

    public interface IPublicacionRepository
    {
        Task<Publicacion> AddPublicacion(Publicacion publicacion);
    }
}
