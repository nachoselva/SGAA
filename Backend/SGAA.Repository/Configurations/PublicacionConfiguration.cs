namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;
    using SGAA.Repository.Extensions;

    internal class PublicacionConfiguration : EntityConfiguration<Publicacion>
    {
        public override void Configure(EntityTypeBuilder<Publicacion> builder)
        {
            base.Configure(builder);

            builder.Property(publicacion => publicacion.MontoAlquiler)
                .IsRequired()
                .DecimalColumn();
            builder.Property(publicacion => publicacion.InicioAlquiler)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DATE);
            builder.Property(publicacion => publicacion.Codigo)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L2);
            builder.Property(publicacion => publicacion.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L1);

            builder
                .HasOne(publicacion => publicacion.Unidad)
                .WithMany(unidad => unidad.Publicaciones)
                .HasPrincipalKey(unidad => unidad.Id)
                .HasForeignKey(propiedad => propiedad.UnidadId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(nameof(Publicacion), tableBuilder =>
                tableBuilder
                .HasCheckConstraintWithEnum(publicacion => publicacion.Status)
            );
        }
    }
}
