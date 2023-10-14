namespace SGAA.Repository.Contracts
{
    using SGAA.Domain.Core;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUnidadRepository
    {
        Task<IReadOnlyCollection<Unidad>> GetUnidadesByPropietario(int propietarioUserId);
        Task<IReadOnlyCollection<Unidad>> GetUnidades();
        Task<Unidad?> GetUnidadById(int unidadId);
        Task<Propiedad?> GetPropiedadByDireccion(int ciudadId, string calle, int altura);
        Task<Unidad?> GetUnidadByDireccion(int ciudadId, string calle, int altura, string piso, string departamento);
        Task<Unidad> AddUnidad(Unidad unidad);
        Task<Unidad> UpdateUnidad(Unidad unidad);
        Task DeleteImagenes(IEnumerable<UnidadImagen> entitiesToDelete);
        Task DeleteTitulares(IEnumerable<Titular> entitiesToDelete);
    }
}
