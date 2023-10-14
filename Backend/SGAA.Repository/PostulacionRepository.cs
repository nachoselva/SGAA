namespace SGAA.Repository
{
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

        public async Task<Postulacion> AddPostulacion(Postulacion postulacion)
        {
            await _dbContext.Postulaciones.AddAsync(postulacion);
            await _dbContext.SaveChangesAsync();
            return postulacion;
        }
    }
}
