namespace SGAA.Models.Base
{
    using SGAA.Domain.Base;

    public interface IPutModel<T>
        where T : IEntity
    {
        public T MapToEntity(T entity);
    }
}
