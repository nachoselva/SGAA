namespace SGAA.Repository
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using SGAA.Domain.Auth;

    public class SGAADbContext : IdentityDbContext<Usuario, Rol, int, UsuarioPermiso, UsuarioRol, UsuarioLogin, RolPermiso, UsuarioToken>
    {
        IConfiguration _configuration;

        public SGAADbContext(DbContextOptions<SGAADbContext> options, IConfiguration configuration)
        : base(options)
        {
            _configuration = configuration;
        }

        public override DbSet<Rol> Roles { get; set; }
        //public DbSet<Usuario> Members { get; set; }
        //public DbSet<Unidad> Homes { get; set; }
        //public DbSet<Ciudad> Cities { get; set; }
        //public DbSet<Provincia> Provinces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SGAADbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"), x => x.UseDateOnlyTimeOnly());
        }
    }
}
