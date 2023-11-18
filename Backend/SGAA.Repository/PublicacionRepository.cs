namespace SGAA.Repository
{
    using Microsoft.EntityFrameworkCore;
    using SGAA.Domain.Core;
    using SGAA.Repository.Contexts;
    using SGAA.Repository.Contracts;
    using System.Threading.Tasks;

    public class PublicacionRepository : IPublicacionRepository
    {
        private readonly SGAADbContext _dbContext;
        public PublicacionRepository(SGAADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IQueryable<Publicacion> PublicacionQuery()
        {
            return _dbContext.Publicaciones
                .Include(p => p.Unidad)
                .ThenInclude(u => u.PropietarioUsuario)
                .Include(p => p.Unidad)
                .ThenInclude(u => u.Detalle)
                .ThenInclude(d => d.Imagenes)
                .Include(p => p.Unidad)
                .ThenInclude(u => u.Propiedad)
                .ThenInclude(u => u.Ciudad)
                .ThenInclude(u => u.Provincia)
                .Include(p => p.Postulaciones)
                .ThenInclude(po => po.Aplicacion)
                .ThenInclude(ap => ap.InquilinoUsuario)
                .OrderByDescending(a => a.Audit.CreatedOn);
        }

        public Task<Publicacion?> GetPublicacion(int publicacionId)
        {
            return PublicacionQuery().FirstOrDefaultAsync(p => p.Id == publicacionId);
        }

        public Task<Publicacion?> GetPublicacion(string codigo)
        {
            return PublicacionQuery().FirstOrDefaultAsync(p => p.Codigo == codigo);
        }

        public async Task<IReadOnlyCollection<Publicacion>> GetPublicaciones()
        {
            return await PublicacionQuery().ToListAsync();
        }

        public async Task<IReadOnlyCollection<Publicacion>> GetPublicacionesByPropietario(int propietarioUsuarioId)
        {
            return await PublicacionQuery()
                .Where(p => p.Unidad.PropietarioUsuarioId == propietarioUsuarioId)
                .ToListAsync();
        }

        public async Task<Publicacion> AddPublicacion(Publicacion publicacion)
        {
            var entityEntry = await _dbContext.Publicaciones.AddAsync(publicacion);
            await _dbContext.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task<Publicacion> UpdatePublicacion(Publicacion publicacion)
        {
            var entityEntry = _dbContext.Publicaciones.Update(publicacion);
            await _dbContext.SaveChangesAsync();
            return entityEntry.Entity;
        }
    }
}
