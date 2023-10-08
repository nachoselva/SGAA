namespace SGAA.Domain.Base
{
    public abstract class BaseAuditableEntity : IAuditableEntity
    {
        public Audit Audit { get; set; } = default!;
    }
}
