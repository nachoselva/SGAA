namespace SGAA.Repository.Contracts
{
    using SGAA.Domain.Core;

    public interface IAplicacionRepository
    {
        Task<IReadOnlyCollection<Aplicacion>> GetAplicaciones(int usuarioId);
        Task<IReadOnlyCollection<Aplicacion>> GetAplicaciones();
        Task<Aplicacion?> GetAplicacion(int aplicacionId);
        Task<Aplicacion> AddAplicacion(Aplicacion aplicacion);
        Task<Aplicacion> UpdateAplicacion(Aplicacion aplicacion);
        Task DeleteGarantias(IEnumerable<Garantia> entitiesToDelete);
        Task DeletePostulantes(IEnumerable<Postulante> entitiesToDelete);
    }
}
