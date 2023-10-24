namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Auth;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;
    using SGAA.Repository.Extensions;

    internal class IndiceConfiguration : EntityConfiguration<Indice>
    {

        public override void Configure(EntityTypeBuilder<Indice> builder)
        {
            base.Configure(builder);

            builder.Property(i => i.Nombre)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LENGTH_L1);

            builder.HasDataFromEnum(p => p.Nombre, enumItem => new Indice(1, enumItem.Option));

            builder.ToTable(nameof(Indice), tableBuilder =>
                tableBuilder
                .HasCheckConstraintWithEnum(indice => indice.Nombre)
            );
        }
    }
}
