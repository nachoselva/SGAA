namespace SGAA.Repository.Contracts
{
    using SGAA.Domain.Core;
    using System.Threading.Tasks;

    public interface IPostulacionRepository
    {
        Task<Postulacion?> GetPostulacionById(int postulacionId);
        Task<Postulacion> AddPostulacion(Postulacion postulacion);
        Task<Postulacion> UpdatePostulacion(Postulacion postulacion);
    }
}
