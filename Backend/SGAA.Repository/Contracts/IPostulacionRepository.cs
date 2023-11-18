namespace SGAA.Repository.Contracts
{
    using SGAA.Domain.Core;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostulacionRepository
    {
        Task<Postulacion?> GetPostulacion(int postulacionId);
        Task<IReadOnlyCollection<Postulacion>> GetPostulaciones(int inquilinoUsuarioId);
        Task<IReadOnlyCollection<Postulacion>> GetPostulaciones();
        Task<Postulacion> AddPostulacion(Postulacion postulacion);
        Task<Postulacion> UpdatePostulacion(Postulacion postulacion);
    }
}
