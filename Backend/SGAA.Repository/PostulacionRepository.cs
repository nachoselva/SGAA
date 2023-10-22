namespace SGAA.Repository
{
    using Microsoft.EntityFrameworkCore;
    using SGAA.Domain.Core;
    using SGAA.Repository.Contexts;
    using SGAA.Repository.Contracts;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PostulacionRepository : IPostulacionRepository
    {
        private readonly SGAADbContext _dbContext;
        public PostulacionRepository(SGAADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IQueryable<Postulacion> PostulacionQuery()
        => _dbContext.Postulaciones
            .Include(p => p.Publicacion)
            .ThenInclude(pu => pu.Unidad)
            .ThenInclude(u => u.PropietarioUsuario)
            .Include(p => p.Publicacion)
            .ThenInclude(pu => pu.Unidad)
            .ThenInclude(u => u.Propiedad)
            .ThenInclude(pr => pr.Ciudad)
            .ThenInclude(c => c.Provincia)
            .Include(p => p.Publicacion)
            .ThenInclude(pu => pu.Unidad)
            .ThenInclude(u => u.Titulares)
            .Include(p => p.Aplicacion)
            .ThenInclude(a => a.InquilinoUsuario)
            .Include(p => p.Aplicacion)
            .ThenInclude(a => a.Postulantes);

        public Task<Postulacion?> GetPostulacion(int postulacionId)
        => PostulacionQuery().Where(p => p.Id == postulacionId).FirstOrDefaultAsync();

        public async Task<Postulacion> AddPostulacion(Postulacion postulacion)
        {
            var entityEntry = await _dbContext.Postulaciones.AddAsync(postulacion);
            await _dbContext.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task<Postulacion> UpdatePostulacion(Postulacion postulacion)
        {
            var entityEntry = _dbContext.Postulaciones.Update(postulacion);
            await _dbContext.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task<IReadOnlyCollection<Postulacion>> GetPostulaciones(int inquilinoUsuarioId)
        {
            return await PostulacionQuery()
                .Where(p => p.Aplicacion.InquilinoUsuarioId == inquilinoUsuarioId)
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<Postulacion>> GetPostulaciones()
        {
            return await PostulacionQuery()
                .ToListAsync();
        }
    }
}
