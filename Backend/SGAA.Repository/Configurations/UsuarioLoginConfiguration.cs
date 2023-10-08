namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Auth;
    using SGAA.Repository.Configuration.Base;

    internal class UsuarioLoginConfiguration : AuditableEntityConfiguration<UsuarioLogin>
    {
        public override void Configure(EntityTypeBuilder<UsuarioLogin> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(UsuarioLogin));

            // Composite primary key consisting of the LoginProvider and the key to use
            // with that provider
            builder.HasKey(l => new { l.LoginProvider, l.ProviderKey });

            // Composite primary key consisting of the LoginProvider and the key to use
            // with that provider
            builder.HasIndex(l => new { l.LoginProvider, l.ProviderKey }, "MemberLoginCompositeKey").IsUnique();

            // Limit the size of the composite key columns due to common DB restrictions
            builder.Property(l => l.LoginProvider).HasMaxLength(128);
            builder.Property(l => l.ProviderKey).HasMaxLength(128);
        }
    }
}
