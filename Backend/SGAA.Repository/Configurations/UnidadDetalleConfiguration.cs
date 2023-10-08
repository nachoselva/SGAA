namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
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

            builder.Property(detalle => detalle.Descripcion)
                .HasMaxLength(DataTypes.TEXT_LENGTH_L4);

            builder
            .HasOne(d => d.Unidad)
            .WithOne(u => u.Detalle)
            .HasPrincipalKey<Unidad>(u => u.Id)
            .HasForeignKey<UnidadDetalle>(f => f.UnidadId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
