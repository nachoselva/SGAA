namespace SGAA.Repository
{
    using Microsoft.EntityFrameworkCore;
    using SGAA.Domain.Core;
    using SGAA.Repository.Contexts;
    using SGAA.Repository.Contracts;
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
            .Include(p => p.Aplicacion)
            .ThenInclude(a => a.InquilinoUsuario);

        public Task<Postulacion?> GetPostulacionById(int postulacionId)
        => PostulacionQuery().Where(p => p.Id == postulacionId).FirstOrDefaultAsync();

        public async Task<Postulacion> AddPostulacion(Postulacion postulacion)
        {
            await _dbContext.Postulaciones.AddAsync(postulacion);
            await _dbContext.SaveChangesAsync();
            return postulacion;
        }

        public async Task<Postulacion> UpdatePostulacion(Postulacion postulacion)
        {
            _dbContext.Postulaciones.Update(postulacion);
            await _dbContext.SaveChangesAsync();
            return postulacion;
        }
    }
}
