namespace SGAA.Models.Base
{
    using SGAA.Domain.Base;

    public interface IPostMapper<T, U>
       where T : IEntity
       where U : IPostModel<T>
    {
        public T ToEntity(U postModel);
    }
}
