namespace SGAA.Repository.Contracts
{
    using SGAA.Domain.Core;
    using System.Threading.Tasks;

    public interface IPostulacionRepository
    {
        Task<Postulacion> AddPostulacion(Postulacion postulacion);
    }
}
