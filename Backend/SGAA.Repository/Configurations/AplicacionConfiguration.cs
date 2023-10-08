namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;
    using SGAA.Repository.Extensions;

    internal class AplicacionConfiguration : EntityConfiguration<Aplicacion>
    {
        public override void Configure(EntityTypeBuilder<Aplicacion> builder)
        {
            base.Configure(builder);

            builder.Property(aplicacion => aplicacion.PuntuacionTotal)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DECIMAL);

            builder.Property(aplicacion => aplicacion.Status)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L1);

            builder
                .HasOne(unidad => unidad.InquilinoUsuario)
                .WithMany(usuario => usuario.Aplicaciones)
                .HasPrincipalKey(usuario => usuario.Id)
                .HasForeignKey(aplicacion => aplicacion.InquilinoUsuarioId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(tableBuilder =>
                tableBuilder
                .HasCheckConstraintWithEnum(aplicacion => aplicacion.Status)
            );
        }
    }
}
