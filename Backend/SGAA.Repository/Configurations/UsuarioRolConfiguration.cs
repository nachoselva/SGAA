namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Auth;
    using SGAA.Repository.Configuration.Base;

    internal class UsuarioRolConfiguration : AuditableEntityConfiguration<UsuarioRol>
    {
        public override void Configure(EntityTypeBuilder<UsuarioRol> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(UsuarioRol));

            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });
        }
    }
}
