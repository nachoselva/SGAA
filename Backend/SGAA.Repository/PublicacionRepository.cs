namespace SGAA.Repository
{
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

        public async Task<Publicacion> AddPublicacion(Publicacion publicacion)
        {
            var entityEntry = await _dbContext.Publicaciones.AddAsync(publicacion);
            await _dbContext.SaveChangesAsync();
            return entityEntry.Entity;
        }
    }
}
