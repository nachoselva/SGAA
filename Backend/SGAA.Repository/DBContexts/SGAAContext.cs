﻿namespace SGAA.Repository.Contexts
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using SGAA.Domain.Auth;
    using SGAA.Domain.Base;
    using SGAA.Domain.Core;
    using SGAA.Utils.Configuration;

    public class SGAADbContext : IdentityDbContext<Usuario, Rol, int, UsuarioPermiso, UsuarioRol, UsuarioLogin, RolPermiso, UsuarioToken>
    {
        private readonly ISGAAConfiguration _configuration;

        public SGAADbContext(DbContextOptions<SGAADbContext> options, ISGAAConfiguration configuration)
        : base(options)
        {
            _configuration = configuration;
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Unidad> Unidades { get; set; }
        public DbSet<UnidadImagen> UnidadImagenes { get; set; }
        public DbSet<Propiedad> Propiedades { get; set; }
        public DbSet<Publicacion> Publicaciones { get; set; }
        public DbSet<Titular> Titulares { get; set; }
        public DbSet<Aplicacion> Aplicaciones { get; set; }
        public DbSet<Postulacion> Postulaciones { get; set; }
        public DbSet<Garantia> Garantias { get; set; }
        public DbSet<Postulante> Postulantes { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Indice> Indices { get; set; }
        public DbSet<IndiceValor> Valores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SGAADbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetDatabaseConnectionString(), x => x.UseDateOnlyTimeOnly());
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            var task = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            return task;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSaving();
            var task = base.SaveChangesAsync(cancellationToken);
            return task;
        }

        private void OnBeforeSaving()
        {
            DateTime now = DateTime.Now;

            foreach (EntityEntry entry in ChangeTracker.Entries()
                .Where(e => !e.Metadata.IsOwned())
                .Where(e => e.Entity is IAuditableEntity))
            {
                IAuditableEntity entity = (IAuditableEntity)entry.Entity;
                entity.Audit ??= new Audit();
                var auditReference = entry.Reference(nameof(entity.Audit))!;
                var property = entry.Reference(nameof(entity.Audit)).TargetEntry!.Property(nameof(entity.Audit.IsDeleted));

                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.Audit.CreatedOn = now;
                        property.CurrentValue = false;
                        break;
                    case EntityState.Modified:
                        entity.Audit.LastModifiedOn = now;
                        property.CurrentValue = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        auditReference.TargetEntry!.State = EntityState.Unchanged;
                        if ((bool)property.CurrentValue! == true)
                            break;
                        entity.Audit.LastModifiedOn = now;
                        property.CurrentValue = true;
                        break;
                }
            }
        }
    }
}
