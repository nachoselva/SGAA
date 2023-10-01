﻿namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;

    internal class PropiedadConfiguration : EntityConfiguration<Propiedad>
    {

        public override void Configure(EntityTypeBuilder<Propiedad> builder)
        {
            base.Configure(builder);

            builder.Property(propiedad => propiedad.Calle)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_NAME_LENGTH);

            builder.Property(propiedad => propiedad.Altura)
                .IsRequired();

            builder
                .HasOne(propiedad => propiedad.Ciudad)
                .WithMany(ciudad => ciudad.Propiedades)
                .HasPrincipalKey(ciudad => ciudad.Id)
                .HasForeignKey(propiedad => propiedad.CiudadId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
