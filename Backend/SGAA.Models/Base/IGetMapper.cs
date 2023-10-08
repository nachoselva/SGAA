namespace SGAA.Models.Base
{
    using SGAA.Domain.Base;

    public interface IGetMapper<T, U>
        where T : IEntity
        where U : IGetModel<T>
    {
        public U ToGetModel(T entity);
    }
}
