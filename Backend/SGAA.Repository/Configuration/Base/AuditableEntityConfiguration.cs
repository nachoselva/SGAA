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
            builder.HasQueryFilter(app => EF.Property<bool>(app, "_isDeleted") == false);

            builder.OwnsOne(entity => entity.Audit, auditProp =>
            {
                auditProp.Property("_isDeleted").HasColumnName("IsDeleted");
                auditProp.Property(p => p.CreatedOn);
                auditProp.Property(p => p.CreatedBy);
                auditProp.Property(p => p.LastModifiedOn);
                auditProp.Property(p => p.LastModifiedBy);
            });
        }
    }
}