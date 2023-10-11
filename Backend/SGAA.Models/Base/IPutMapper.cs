namespace SGAA.Models.Base
{
    using SGAA.Domain.Base;

    public interface IPutMapper<T, U>
       where T : IEntity
       where U : IPutModel<T>
    {
        public T ToEntity(U putModel, T entity);
    }
}
