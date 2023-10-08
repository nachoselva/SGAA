namespace SGAA.Models.Base
{
    using SGAA.Domain.Base;

    public interface IPutMapper<T>
       where T : IEntity
    {
        public T FromUpdateModel(IPutModel<T> entity);
    }
}
