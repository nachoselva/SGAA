namespace SGAA.Domain.Base
{
    public interface IEntity : IAuditableEntity
    {
        int Id { get; }
    }
}
