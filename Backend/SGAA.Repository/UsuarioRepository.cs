namespace SGAA.Repository
{
    using Microsoft.EntityFrameworkCore;
    using SGAA.Domain.Auth;
    using SGAA.Repository.Contexts;
    using SGAA.Repository.Contracts;

    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SGAADbContext _dbContext;

        public UsuarioRepository(SGAADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<Usuario>> GetUsuarios()
        {
            return await _dbContext.Users.ToListAsync();
        }
    }
}
