namespace SGAA.Repository.Contexts
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.Extensions.Configuration;
    using SGAA.Domain.Auth;
    using SGAA.Domain.Base;
    using SGAA.Domain.Core;

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
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SGAADbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"), x => x.UseDateOnlyTimeOnly());
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
            CancellationToken cancellationToken = default(CancellationToken))
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
            DateTime now = DateTime.UtcNow;

            foreach (EntityEntry entry in ChangeTracker.Entries().Where(e => !e.Metadata.IsOwned()))

                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity is IEntity addedEntity)
                        {
                            //this is required to attach audit details for entities inserted as child /navigation properties.
                            if (addedEntity.Audit == null)
                                addedEntity.Audit = new Audit
                                {
                                    CreatedOn = now
                                };
                            else
                            {
                                addedEntity.Audit.CreatedOn = now;
                            }

                            entry.Property("_isDeleted").CurrentValue = false;
                        }
                        break;
                    case EntityState.Modified:
                        if (entry.Entity is IEntity modifiedEntity)
                        {
                            if (modifiedEntity.Audit == null)
                                modifiedEntity.Audit = new Audit();

                            modifiedEntity.Audit.LastModifiedOn = now;

                            entry.Property("_isDeleted").CurrentValue = false;
                        }
                        break;
                    case EntityState.Deleted:
                        // *** Logical Delete ***
                        // Making an entity state Modified is like updating all the Properties.
                        // With Unchanged when we change a property value the entity is mark as modified
                        // but only that property/es is/are updated.
                        // In this case we want to update only the IsDeleted property and AuditDetails
                        entry.State = EntityState.Unchanged;

                        // AuditDetails are marked as deleted and this add all the properties to the update query
                        // when setting LastModifiedOn and LastModifiedBy values.
                        ReferenceEntry auditEntry = entry.References
                            .FirstOrDefault(x => x.Metadata.ClrType == typeof(Audit))!;

                        if (auditEntry != null)
                            auditEntry.TargetEntry!.State = EntityState.Unchanged;

                        if (entry.Entity is IEntity deletedEntity)
                        {
                            if (entry.Property("_isDeleted").CurrentValue is bool value && value)
                                break;

                            if (deletedEntity.Audit == null)
                                deletedEntity.Audit = new Audit();

                            deletedEntity.Audit.LastModifiedOn = now;

                            entry.Property("_isDeleted").CurrentValue = true;
                        }
                        break;
                }
        }
    }
}
