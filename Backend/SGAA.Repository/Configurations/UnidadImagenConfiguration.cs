namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;

    internal class UnidadImagenConfiguration : EntityConfiguration<UnidadImagen>
    {
        public override void Configure(EntityTypeBuilder<UnidadImagen> builder)
        {
            base.Configure(builder);

            builder.Property(i => i.Titulo)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L3);

            builder.Property(i => i.Descripcion)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L5);

            builder.Property(i => i.Archivo)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_FILE);

            builder
            .HasOne(i => i.Detalle)
            .WithMany(d => d.Imagenes)
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(i => i.UnidadDetalleId);

            builder.ToTable(nameof(UnidadImagen));
        }
    }
}
