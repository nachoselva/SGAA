namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;

    internal class FirmaConfiguration : EntityConfiguration<Firma>
    {
        public override void Configure(EntityTypeBuilder<Firma> builder)
        {
            base.Configure(builder);
        }
    }
}
