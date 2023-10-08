namespace SGAA.Models
{
    using Base;
    using SGAA.Domain.Base;

    public static class Extensions
    {
        public static U MapToGetModel<T, U>(this T entity, IGetMapper<T, U> mapper)
            where T : IEntity
            where U : IGetModel<T> => mapper.MapFromEntity(entity);

        public static T ToEntity<T>(this IAddModel<T> addModel)
            where T : IEntity => addModel.MapToEntity();

        public static T ToEntity<T>(this IUpdateModel<T> updateModel, T entity)
            where T : IEntity => updateModel.MapToEntity(entity);
    }
}
