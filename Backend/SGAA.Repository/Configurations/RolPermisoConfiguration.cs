namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Auth;
    using SGAA.Repository.Configuration.Base;

    internal class RolPermisoConfiguration : EntityConfiguration<RolPermiso>
    {

        public override void Configure(EntityTypeBuilder<RolPermiso> builder)
        {
            base.Configure(builder);
            builder.ToTable(nameof(RolPermiso));
        }
    }
}
