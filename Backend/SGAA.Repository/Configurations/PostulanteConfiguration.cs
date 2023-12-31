﻿namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;

    internal class PostulanteConfiguration : EntityConfiguration<Postulante>
    {

        public override void Configure(EntityTypeBuilder<Postulante> builder)
        {
            base.Configure(builder);
            builder.Property(postulante => postulante.Nombre)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L3);
            builder.Property(postulante => postulante.Apellido)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L3);
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L4);
            builder.Property(postulante => postulante.NumeroIdentificacion)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L1);
            builder.Property(postulante => postulante.FechaNacimiento)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DATE);
            builder.Property(postulante => postulante.Domicilio)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L4);
            builder.Property(postulante => postulante.FrenteIdentificacionArchivo);
            builder.Property(postulante => postulante.DorsoIdentificacionArchivo);
            builder.Property(postulante => postulante.FechaEmpleadoDesde)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DATE);
            builder.Property(postulante => postulante.NombreEmpresa)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L3);
            builder.Property(postulante => postulante.IngresoMensual)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DECIMAL);
            builder.Property(postulante => postulante.ReciboDeSueldoArchivo);

            builder
                .HasOne(postulacion => postulacion.Aplicacion)
                .WithMany(aplicacion => aplicacion.Postulantes)
                .HasPrincipalKey(aplicacion => aplicacion.Id)
                .HasForeignKey(postulante => postulante.AplicacionId);

            builder.ToTable(nameof(Postulante));

        }
    }
}
