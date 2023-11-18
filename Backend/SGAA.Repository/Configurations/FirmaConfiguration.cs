namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;
    using SGAA.Repository.Extensions;

    internal class FirmaConfiguration : EntityConfiguration<Firma>
    {
        public override void Configure(EntityTypeBuilder<Firma> builder)
        {
            base.Configure(builder);

            builder.Property(firma => firma.FechaFirma)
                .HasColumnType(DataTypes.TYPE_DATETIME);
            builder.Property(firma => firma.DireccionIp)
                .HasMaxLength(DataTypes.TEXT_LENGTH_L1);
            builder.Property(unidad => unidad.Rol)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L1);
            builder.Property(firma => firma.NumeroIdentificacion)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L1);

            builder
            .HasOne(f => f.Contrato)
            .WithMany(c => c.Firmas)
            .HasPrincipalKey(r => r.Id)
            .HasForeignKey(ur => ur.ContratoId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(f => f.Usuario)
            .WithMany(u => u.Firmas)
            .HasPrincipalKey(u => u.Id)
            .HasForeignKey(f => f.UsuarioId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            builder.Property(firma => firma.Rol)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L1);

            builder.ToTable(tableBuilder =>
            {
                tableBuilder
                .HasCheckConstraintWithEnum(firma => firma.Rol);
            });
        }
    }
}
