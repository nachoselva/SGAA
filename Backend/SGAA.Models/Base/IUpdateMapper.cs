namespace SGAA.Models.Base
{
    using SGAA.Domain.Base;

    public interface IUpdateMapper<T>
       where T : IEntity
    {
        public T FromUpdateModel(IUpdateModel<T> entity);
    }
}
