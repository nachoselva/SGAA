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

        private IQueryable<Usuario> UsuarioQuery()
        => _dbContext.Users
            .Include(u => u.UsuarioRoles)
            .ThenInclude(u => u.Rol);

        public async Task<IReadOnlyCollection<Usuario>> GetUsuarios()
        => await UsuarioQuery()
             .ToListAsync();
    }
}
