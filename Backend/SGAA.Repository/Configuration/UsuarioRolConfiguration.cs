namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Auth;
    using SGAA.Repository.Configuration.Base;

    internal class UsuarioRolConfiguration : AuditableEntityConfiguration<UsuarioRol>
    {
        public override void Configure(EntityTypeBuilder<UsuarioRol> builder)
        {
            base.Configure(builder);
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });
        }
    }
}
