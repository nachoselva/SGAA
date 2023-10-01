namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;

    internal class UnidadImagenConfiguration : EntityConfiguration<UnidadImagen>
    {
        public override void Configure(EntityTypeBuilder<UnidadImagen> builder)
        {
            base.Configure(builder);
        }
    }
}
