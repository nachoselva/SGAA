namespace SGAA.Repository.Configuration.Base
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Base;

    public abstract class EntityConfiguration<T> : AuditableEntityConfiguration<T>
        where T : class, IEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);
            builder.HasKey(e => e.Id);
        }
    }
}