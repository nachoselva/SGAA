namespace SGAA.Repository.Configuration
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
                .HasMaxLength(DataTypes.TEXT_NAME_LENGTH);
            builder.Property(postulante => postulante.Apellido)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_NAME_LENGTH);
            builder.Property(postulante => postulante.TipoIdentificacion)
                .IsRequired();
            builder.Property(postulante => postulante.NumeroIdentificacion)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_SHORTEST_NAME_LENGTH);
            builder.Property(postulante => postulante.FechaNacimiento)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DATE);
            builder.Property(postulante => postulante.Domicilio)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LONG_NAME_LENGTH);
            builder.Property(postulante => postulante.FrenteIdentificacionArchivo)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_FILE);
            builder.Property(postulante => postulante.DorsoIdentificacionArchivo)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_FILE);
            builder.Property(postulante => postulante.FechaEmpleadoDesde)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DATE);
            builder.Property(postulante => postulante.NombreEmpresa)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_NAME_LENGTH);
            builder.Property(postulante => postulante.IngresoMensual)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DECIMAL);
            builder.Property(postulante => postulante.ReciboDeSueldoArchivo)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_FILE);

            builder
                .HasOne(postulacion => postulacion.Aplicacion)
                .WithMany(aplicacion => aplicacion.Postulantes)
                .HasPrincipalKey(aplicacion => aplicacion.Id)
                .HasForeignKey(postulante => postulante.AplicacionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
