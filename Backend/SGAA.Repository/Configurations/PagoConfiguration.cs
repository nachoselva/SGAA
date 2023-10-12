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

            builder.Property(pago => pago.Descripcion)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L3);

            builder.Property(pago => pago.Monto)
                .IsRequired()
                .DecimalColumn();

            builder.Property(pago => pago.FechaVencimiento)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DATE);

            builder.Property(firma => firma.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L1);

            builder.Property(pago => pago.FechaPago)
                .HasColumnType(DataTypes.TYPE_DATETIME);

            builder
                .HasOne(p => p.Contrato)
                .WithMany(c => c.Pagos)
                .HasPrincipalKey(r => r.Id)
                .HasForeignKey(ur => ur.ContratoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(tableBuilder =>
                tableBuilder
                .HasCheckConstraintWithEnum(pago => pago.Status)
            );
        }
    }
}
