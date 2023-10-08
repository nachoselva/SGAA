namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Auth;
    using SGAA.Repository.Configuration.Base;

    internal class UsuarioPermisoConfiguration : EntityConfiguration<UsuarioPermiso>
    {

        public override void Configure(EntityTypeBuilder<UsuarioPermiso> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(UsuarioPermiso));
        }
    }
}
