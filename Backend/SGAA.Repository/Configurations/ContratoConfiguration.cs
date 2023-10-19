namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;
    using SGAA.Repository.Extensions;

    internal class ContratoConfiguration : EntityConfiguration<Contrato>
    {
        public override void Configure(EntityTypeBuilder<Contrato> builder)
        {
            base.Configure(builder);

            builder.Property(contrato => contrato.FechaDesde)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DATE);

            builder.Property(contrato => contrato.FechaHasta)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DATE);

            builder.Property(contrato => contrato.FechaCancelacion)
                .HasColumnType(DataTypes.TYPE_DATE);

            builder.Property(contrato => contrato.MontoAlquiler)
               .IsRequired()
               .DecimalColumn();

            builder.Property(contrato => contrato.OrdenRenovacion)
                .IsRequired();

            builder.Property(contrato => contrato.Archivo)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_FILE);

            builder.Property(contrato => contrato.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L1);

            builder.ToTable(nameof(Contrato), tableBuilder =>
                tableBuilder
                .HasCheckConstraintWithEnum(contrato => contrato.Status));

        }
    }
}
