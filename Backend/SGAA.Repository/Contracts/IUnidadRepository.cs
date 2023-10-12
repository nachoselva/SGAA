namespace SGAA.Repository.Contracts
{
    using SGAA.Domain.Core;
    using System.Threading.Tasks;

    public interface IUnidadRepository
    {
        Task<Unidad> AddUnidad(Unidad unidad);
        Task<Propiedad?> GetPropiedadByDireccion(int ciudadId, string calle, int altura);
        Task<Unidad?> GetUnidadByDireccion(int ciudadId, string calle, int altura, string piso, string departamento);
        Task<Unidad?> GetUnidadById(int unidadId);
    }
}
