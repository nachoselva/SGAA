namespace SGAA.Models.Base
{
    using SGAA.Domain.Base;

    public interface IUpdateModel<T>
        where T : IEntity
    {
        public T MapToEntity(T entity);
    }
}
