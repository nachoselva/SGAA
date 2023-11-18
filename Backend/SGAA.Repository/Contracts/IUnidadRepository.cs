namespace SGAA.Repository.Contracts
{
    using SGAA.Domain.Core;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUnidadRepository
    {
        Task<IReadOnlyCollection<Unidad>> GetUnidades(int propietarioUserId);
        Task<IReadOnlyCollection<Unidad>> GetUnidades();
        Task<Unidad?> GetUnidad(int unidadId);
        Task<Propiedad?> GetPropiedad(int ciudadId, string calle, int altura);
        Task<Unidad?> GetUnidad(int ciudadId, string calle, int altura, string piso, string departamento);
        Task<Unidad> AddUnidad(Unidad unidad);
        Task<Unidad> UpdateUnidad(Unidad unidad);
        Task DeleteImagenes(IEnumerable<UnidadImagen> entitiesToDelete);
        Task DeleteTitulares(IEnumerable<Titular> entitiesToDelete);
    }
}
