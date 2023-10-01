namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Auth;
    using SGAA.Repository.Configuration.Base;
    using SGAA.Repository.Extensions;

    internal class RolConfiguration : EntityConfiguration<Rol>
    {
        public override void Configure(EntityTypeBuilder<Rol> builder)
        {
            base.Configure(builder);

            // Index for "normalized" role name to allow efficient lookups
            builder.HasIndex(r => r.NormalizedName, "RoleNameIndex").IsUnique();

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(r => r.Name).HasMaxLength(256);
            builder.Property(u => u.NormalizedName).HasMaxLength(256);

            builder.Property(r => r.RolType).IsRequired().HasMaxLength(20).HasConversion<string>();

            // The relationships between Role and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each Role can have many entries in the UserRole join table
            builder.HasMany(r => r.UsuarioRoles).WithOne(mr => mr.Rol).HasForeignKey(ur => ur.RoleId).IsRequired();

            // Each Role can have many associated RoleClaims
            builder.HasMany(r => r.Permisos).WithOne(rc => rc.Rol).HasForeignKey(ur => ur.RoleId).IsRequired();

            builder.ToTable(tableBuilder =>
                tableBuilder
                .HasCheckConstraintWithEnum(rol => rol.RolType)
            );

            builder.HasDataFromEnum(r => r.RolType, enumItem => new Rol(enumItem.Id, enumItem.Option, enumItem.Name, enumItem.Name.ToUpper()));
        }
    }
}
