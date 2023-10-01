namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;

    internal class IndiceValorConfiguration : EntityConfiguration<IndiceValor>
    {
        public override void Configure(EntityTypeBuilder<IndiceValor> builder)
        {
            base.Configure(builder);

            builder.Property(indicevalor => indicevalor.FechaDesde)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DATE);

            builder.Property(indicevalor => indicevalor.Valor)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DECIMAL);

            builder
                .HasOne(valor => valor.Indice)
                .WithMany(indice => indice.Valores)
                .HasPrincipalKey(indice => indice.Id)
                .HasForeignKey(valor => valor.IndiceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
