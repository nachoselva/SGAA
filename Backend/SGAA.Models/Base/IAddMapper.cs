namespace SGAA.Models.Base
{
    using SGAA.Domain.Base;

    public interface IAddMapper<T>
       where T : IEntity
    {
        public T FromAddModel(IAddModel<T> entity);
    }
}
