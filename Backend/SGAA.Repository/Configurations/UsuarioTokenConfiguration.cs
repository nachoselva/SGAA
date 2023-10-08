namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Auth;
    using SGAA.Repository.Configuration.Base;

    internal class UsuarioTokenConfiguration : AuditableEntityConfiguration<UsuarioToken>
    {
        public override void Configure(EntityTypeBuilder<UsuarioToken> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(UsuarioToken));

            // Composite primary key consisting of the UserId, LoginProvider and Name
            builder.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            // Composite primary key consisting of the UserId, LoginProvider and Name
            builder.HasIndex(t => new { t.UserId, t.LoginProvider, t.Name }, "MemberTokenCompositeKey").IsUnique();

            // Limit the size of the composite key columns due to common DB restrictions
            builder.Property(t => t.LoginProvider).HasMaxLength(128);
            builder.Property(t => t.Name).HasMaxLength(256);
        }
    }
}
