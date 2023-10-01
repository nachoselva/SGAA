namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;
    using SGAA.Repository.Extensions;

    internal class PagoConfiguration : EntityConfiguration<Pago>
    {
        public override void Configure(EntityTypeBuilder<Pago> builder)
        {
            base.Configure(builder);

            builder.Property(pago => pago.Monto)
                .IsRequired()
                .DecimalColumn();

            builder.Property(pago => pago.FechaVencimiento)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DATE);
        }
    }
}
