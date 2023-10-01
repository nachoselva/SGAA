namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;
    using SGAA.Repository.Extensions;

    internal class UnidadDetalleConfiguration : EntityConfiguration<UnidadDetalle>
    {
        public override void Configure(EntityTypeBuilder<UnidadDetalle> builder)
        {
            base.Configure(builder);

            builder.Property(detalle => detalle.Superficie)
                .IsRequired()
                .DecimalColumn();
        }
    }
}
