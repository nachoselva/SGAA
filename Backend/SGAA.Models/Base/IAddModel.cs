namespace SGAA.Models.Base
{
    using SGAA.Domain.Base;

    public interface IAddModel<T>
        where T : IEntity
    {
        public T MapToEntity();
    }
}
