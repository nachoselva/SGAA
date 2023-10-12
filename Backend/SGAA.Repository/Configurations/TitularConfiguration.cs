﻿namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;

    internal class TitularConfiguration : EntityConfiguration<Titular>
    {
        public override void Configure(EntityTypeBuilder<Titular> builder)
        {
            base.Configure(builder);
            builder.Property(titular => titular.Nombre)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L3);
            builder.Property(titular => titular.Apellido)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L3);
            builder.Property(titular => titular.TipoIdentificacion)
                .HasConversion<string>()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L1)
                .IsRequired();
            builder.Property(titular => titular.NumeroIdentificacion)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L1);
            builder.Property(titular => titular.FechaNacimiento)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DATE);
            builder.Property(titular => titular.Domicilio)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L4);
            builder.Property(titular => titular.FrenteIdentificacionArchivo)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_FILE);
            builder.Property(titular => titular.DorsoIdentificacionArchivo)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_FILE);

            builder
                .HasOne(titular => titular.Unidad)
                .WithMany(unidad => unidad.Titulares)
                .HasPrincipalKey(unidad => unidad.Id)
                .HasForeignKey(titular => titular.UnidadId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
