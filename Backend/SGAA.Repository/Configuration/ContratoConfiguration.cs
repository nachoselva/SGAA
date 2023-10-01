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

            builder.Property(contrato => contrato.MontoAlquiler)
               .IsRequired()
               .DecimalColumn();
        }
    }
}
