﻿namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;
    using SGAA.Repository.Extensions;

    internal class PostulacionConfiguration : EntityConfiguration<Postulacion>
    {
        public override void Configure(EntityTypeBuilder<Postulacion> builder)
        {
            base.Configure(builder);

            builder.Property(postulacion => postulacion.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L1);

            builder.Property(postulacion => postulacion.FechaOferta)
                .HasColumnType(DataTypes.TYPE_DATETIME);

            builder
                .HasOne(postulacion => postulacion.Publicacion)
                .WithMany(publicacion => publicacion.Postulaciones)
                .HasPrincipalKey(publicacion => publicacion.Id)
                .HasForeignKey(postulacion => postulacion.PublicacionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(postulacion => postulacion.Aplicacion)
                .WithMany(aplicacion => aplicacion.Postulaciones)
                .HasPrincipalKey(aplicacion => aplicacion.Id)
                .HasForeignKey(postulacion => postulacion.AplicacionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable(nameof(Postulacion), tableBuilder =>
                tableBuilder
                .HasCheckConstraintWithEnum(postulacion => postulacion.Status)
            );
        }
    }
}
