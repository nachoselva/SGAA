namespace SGAA.Repository
{
    using Microsoft.EntityFrameworkCore;
    using SGAA.Domain.Core;
    using SGAA.Repository.Contexts;
    using SGAA.Repository.Contracts;
    using System.Threading.Tasks;

    internal class UnidadRepository : IUnidadRepository
    {
        private readonly SGAADbContext _dbContext;
        public UnidadRepository(SGAADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unidad> AddUnidad(Unidad unidad)
        {
            var entityEntry = await _dbContext.Unidades.AddAsync(unidad);
            await _dbContext.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public Task<Propiedad?> GetPropiedadByDireccion(int ciudadId, string calle, int altura)
        {
            return _dbContext.Propiedades
                .FirstOrDefaultAsync(p => p.CiudadId == ciudadId && p.Altura == altura && EF.Functions.Like(calle, p.Calle));
        }

        public Task<Unidad?> GetUnidadByDireccion(int ciudadId, string calle, int altura, string piso, string departamento)
        {
            return _dbContext.Unidades
                .Where(u => u.Propiedad.CiudadId == ciudadId && u.Propiedad.Altura == altura && EF.Functions.Like(calle, u.Propiedad.Calle))
                .Where(u => EF.Functions.Like(piso, u.Piso) && EF.Functions.Like(piso, u.Departamento))
                .FirstOrDefaultAsync();
        }

        public Task<Unidad?> GetUnidadById(int unidadId)
        {
            return _dbContext.Unidades.FirstOrDefaultAsync(p => p.Id == unidadId);
        }
    }
}
