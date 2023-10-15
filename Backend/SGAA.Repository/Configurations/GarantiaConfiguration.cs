namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;

    internal class GarantiaConfiguration : EntityConfiguration<Garantia>
    {
        public override void Configure(EntityTypeBuilder<Garantia> builder)
        {
            base.Configure(builder);

            builder.Property(garantia => garantia.Monto)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DECIMAL);

            builder.Property(garantia => garantia.Archivo)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_FILE);

            builder
                .HasOne(garantia => garantia.Aplicacion)
                .WithMany(aplicacion => aplicacion.Garantias)
                .HasPrincipalKey(aplicacion => aplicacion.Id)
                .HasForeignKey(garantia => garantia.AplicacionId);

            builder.ToTable(nameof(Garantia));
        }
    }
}
