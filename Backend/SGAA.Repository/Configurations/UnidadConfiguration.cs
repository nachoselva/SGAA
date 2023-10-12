namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;
    using SGAA.Repository.Extensions;

    internal class UnidadConfiguration : EntityConfiguration<Unidad>
    {
        public override void Configure(EntityTypeBuilder<Unidad> builder)
        {
            base.Configure(builder);
            builder.Property(unidad => unidad.Piso)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L1);
            builder.Property(unidad => unidad.Departamento)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L1);
            builder.Property(unidad => unidad.FechaAdquisicion)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DATE);
            builder.Property(garantia => garantia.TituloPropiedadArchivo)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_FILE);
            builder.Property(unidad => unidad.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L1);

            builder.HasIndex(unidad => new { unidad.PropiedadId, unidad.Piso, unidad.Departamento })
                .IsUnique();

            builder
                .HasOne(unidad => unidad.Propiedad)
                .WithMany(propiedad => propiedad.Unidades)
                .HasPrincipalKey(propiedad => propiedad.Id)
                .HasForeignKey(unidad => unidad.PropiedadId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(unidad => unidad.PropietarioUsuario)
                .WithMany(usuario => usuario.Unidades)
                .HasPrincipalKey(usuario => usuario.Id)
                .HasForeignKey(unidad => unidad.PropiedadId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(nameof(Unidad), tableBuilder =>
                tableBuilder
                .HasCheckConstraintWithEnum(unidad => unidad.Status)
            );
        }
    }
}
