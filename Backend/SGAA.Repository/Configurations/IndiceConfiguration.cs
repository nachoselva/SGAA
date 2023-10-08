namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;
    using SGAA.Repository.Extensions;

    internal class IndiceConfiguration : EntityConfiguration<Indice>
    {

        public override void Configure(EntityTypeBuilder<Indice> builder)
        {
            base.Configure(builder);

            builder.ToTable(tableBuilder =>
                tableBuilder
                .HasCheckConstraintWithEnum(indice => indice.Nombre)
            );
        }
    }
}
