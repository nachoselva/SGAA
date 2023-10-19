namespace SGAA.Repository
{
    using Microsoft.EntityFrameworkCore;
    using SGAA.Domain.Core;
    using SGAA.Repository.Contexts;
    using SGAA.Repository.Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UnidadRepository : IUnidadRepository
    {
        private readonly SGAADbContext _dbContext;
        public UnidadRepository(SGAADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IQueryable<Unidad> UnidadQuery()
        =>
            _dbContext.Unidades
            .Include(u => u.Propiedad)
            .ThenInclude(p => p.Ciudad)
            .ThenInclude(c => c.Provincia)
            .Include(u => u.Detalle)
            .ThenInclude(d => d.Imagenes)
            .Include(u => u.Publicaciones)
            .Include(u => u.PropietarioUsuario)
            .Include(u => u.Comentarios)
            .Include(u => u.Titulares);

        public async Task<IReadOnlyCollection<Unidad>> GetUnidadesByPropietario(int propietarioUserId)
        {
            return await UnidadQuery().Where(u => u.PropietarioUsuarioId == propietarioUserId).ToListAsync();
        }

        public async Task<IReadOnlyCollection<Unidad>> GetUnidades()
        {
            return await UnidadQuery().ToListAsync();
        }

        public Task<Unidad?> GetUnidadById(int unidadId)
        {
            return UnidadQuery().FirstOrDefaultAsync(p => p.Id == unidadId);
        }

        public Task<Propiedad?> GetPropiedadByDireccion(int ciudadId, string calle, int altura)
        {
            return _dbContext.Propiedades
                .FirstOrDefaultAsync(p => p.CiudadId == ciudadId && p.Altura == altura && EF.Functions.Like(calle, p.Calle));
        }

        public Task<Unidad?> GetUnidadByDireccion(int ciudadId, string calle, int altura, string piso, string departamento)
        {
            return UnidadQuery()
                .Where(u => u.Propiedad.CiudadId == ciudadId && u.Propiedad.Altura == altura && EF.Functions.Like(calle, u.Propiedad.Calle))
                .Where(u => EF.Functions.Like(piso, u.Piso) && EF.Functions.Like(departamento, u.Departamento))
                .FirstOrDefaultAsync();
        }

        public async Task<Unidad> AddUnidad(Unidad unidad)
        {
            unidad.PropietarioUsuario = await _dbContext.Usuarios.FirstAsync(usuario => usuario.Id == unidad.PropietarioUsuarioId);
            var entityEntry = await _dbContext.Unidades.AddAsync(unidad);
            await _dbContext.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task<Unidad> UpdateUnidad(Unidad unidad)
        {
            _dbContext.Unidades.Update(unidad);
            await _dbContext.SaveChangesAsync();
            return unidad;
        }

        public async Task DeleteImagenes(IEnumerable<UnidadImagen> entitiesToDelete)
        {
            IEnumerable<int> idsToDelete = entitiesToDelete.Select(e => e.Id);
            _dbContext.UnidadImagenes.RemoveRange(_dbContext.UnidadImagenes.Where(img => idsToDelete.Contains(img.Id)));
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTitulares(IEnumerable<Titular> entitiesToDelete)
        {
            IEnumerable<int> idsToDelete = entitiesToDelete.Select(e => e.Id);
            _dbContext.Titulares.RemoveRange(_dbContext.Titulares.Where(img => idsToDelete.Contains(img.Id)));
            await _dbContext.SaveChangesAsync();
        }
    }
}
