namespace SGAA.Repository.DBContexts
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using SGAA.Domain.Auth;
    using SGAA.Repository.Contexts;
    using System.Threading;
    using System.Threading.Tasks;

    public class SGAAUserStore : UserStore<Usuario, Rol, SGAADbContext, int, UsuarioPermiso, UsuarioRol, UsuarioLogin, UsuarioToken, RolPermiso>
    {

        public SGAAUserStore(SGAADbContext dbContext) : base(dbContext)
        {

        }

        public override Task AddToRoleAsync(Usuario user, string normalizedRoleName, CancellationToken cancellationToken = default)
        {
            return base.AddToRoleAsync(user, normalizedRoleName, cancellationToken);
        }
    }
}
