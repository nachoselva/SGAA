namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;

    internal class UnidadComentarioConfiguration : EntityConfiguration<UnidadComentario>
    {
        public override void Configure(EntityTypeBuilder<UnidadComentario> builder)
        {
            base.Configure(builder);
            builder.Property(comentario => comentario.Comentario)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L5);

            builder.Property(comentario => comentario.Fecha)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DATETIME);

            builder
                .HasOne(comentario => comentario.Unidad)
                .WithMany(unidad => unidad.Comentarios)
                .HasPrincipalKey(unidad => unidad.Id)
                .HasForeignKey(comentario => comentario.UnidadId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
