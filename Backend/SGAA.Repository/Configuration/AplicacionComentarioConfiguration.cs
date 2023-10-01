namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;

    internal class AplicacionComentarioConfiguration : EntityConfiguration<AplicacionComentario>
    {
        public override void Configure(EntityTypeBuilder<AplicacionComentario> builder)
        {
            base.Configure(builder);

            builder.Property(comentario => comentario.Comentario)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L5);

            builder.Property(comentario => comentario.Fecha)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DATETIME);

            builder
                .HasOne(comentario => comentario.Aplicacion)
                .WithMany(aplicacion => aplicacion.Comentarios)
                .HasPrincipalKey(aplicacion => aplicacion.Id)
                .HasForeignKey(comentario => comentario.AplicacionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
