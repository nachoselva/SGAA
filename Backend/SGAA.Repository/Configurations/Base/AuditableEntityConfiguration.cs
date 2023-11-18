namespace SGAA.Repository.Configuration.Base
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Base;

    public abstract class AuditableEntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : class, IAuditableEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.OwnsOne(entity => entity.Audit, auditProp =>
            {
                auditProp.Property(p => p.IsDeleted).HasColumnName(nameof(Audit.IsDeleted)).HasDefaultValue(false);
                auditProp.Property(p => p.CreatedOn).HasColumnName(nameof(Audit.CreatedOn)).HasDefaultValueSql("GETUTCDATE()");
                auditProp.Property(p => p.LastModifiedOn).HasColumnName(nameof(Audit.LastModifiedOn));
            });

            builder.HasQueryFilter(app => app.Audit.IsDeleted == false);
        }
    }
}