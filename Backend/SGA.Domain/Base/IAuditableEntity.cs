namespace SGAA.Domain.Base
{
    public interface IAuditableEntity
    {
        Audit Audit { get; }
    }
}
