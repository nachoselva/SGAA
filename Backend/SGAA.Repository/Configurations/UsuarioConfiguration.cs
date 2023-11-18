namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Auth;
    using SGAA.Repository.Configuration.Base;

    internal class UsuarioConfiguration : EntityConfiguration<Usuario>
    {
        public override void Configure(EntityTypeBuilder<Usuario> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(Usuario));

            // Indexes for "normalized" username and email, to allow efficient lookups
            builder.HasIndex(u => u.NormalizedUserName, "UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail, "EmailIndex");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(256);
            builder.Property(u => u.NormalizedUserName).IsRequired().HasMaxLength(256);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(256);
            builder.Property(u => u.NormalizedEmail).IsRequired().HasMaxLength(256);

            builder.Property(u => u.Nombre).IsRequired().HasMaxLength(256);
            builder.Property(u => u.Apellido).IsRequired().HasMaxLength(256);
            builder.Property(u => u.RefreshToken).HasMaxLength(100);
            builder.Property(u => u.RefreshTokenExpiryTime).HasColumnType("smalldatetime");

            builder.Property(firma => firma.Licencia)
                .IsRequired()
                .HasConversion<string>()
                .HasDefaultValue(Licencia.HabilitacionProfesional)
                .HasMaxLength(DataTypes.TEXT_LENGTH_L1);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            builder.HasMany(m => m.UsuarioPermisos).WithOne(mc => mc.Usuario).HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            builder.HasMany(m => m.UsuarioLogins).WithOne(ml => ml.Usuario).HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            builder.HasMany(m => m.UsuarioTokens).WithOne(mt => mt.Usuario).HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany(m => m.UsuarioRoles).WithOne(mr => mr.Usuario).HasForeignKey(ur => ur.UserId).IsRequired();
        }
    }
}
