namespace SGAA.Domain.Base
{
    public abstract class BaseEntity : BaseAuditableEntity, IEntity
    {
        public int Id { get; protected set; }
    }
}
