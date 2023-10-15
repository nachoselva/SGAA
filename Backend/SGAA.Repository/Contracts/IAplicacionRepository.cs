namespace SGAA.Repository.Contracts
{
    using SGAA.Domain.Core;

    public interface IAplicacionRepository
    {
        Task<IReadOnlyCollection<Aplicacion>> GetAplicacionesByInquilinoUsuarioId(int usuarioId);
        Task<Aplicacion> AddAplicacion(Aplicacion aplicacion);
        Task<Aplicacion> UpdateAplicacion(Aplicacion aplicacion);
        Task DeleteGarantias(IEnumerable<Garantia> entitiesToDelete);
        Task DeletePostulantes(IEnumerable<Postulante> entitiesToDelete);
    }
}
