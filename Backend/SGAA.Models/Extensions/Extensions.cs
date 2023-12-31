﻿namespace SGAA.Models
{
    using Base;
    using SGAA.Domain.Base;

    public static class Extensions
    {
        public static U MapToGetModel<T, U>(this T entity, IGetMapper<T, U> mapper)
            where T : IEntity
            where U : IGetModel<T> => mapper.ToGetModel(entity);

        public static T ToEntity<T, U>(this U addModel, IPostMapper<T, U> mapper)
            where T : IEntity
            where U : IPostModel<T> => mapper.ToEntity(addModel);

        public static T ToEntity<T, U>(this U updateModel, IPutMapper<T, U> mapper, T entity)
            where T : IEntity
            where U : IPutModel<T> => mapper.ToEntity(updateModel, entity);
    }
}
