namespace SGAA.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ReflectionTools
    {
        public static IEnumerable<Type> GetChildrenFromClass<T>()
        {
            return System.Reflection.Assembly.GetAssembly(typeof(T))!.GetTypes()
                .Where(type => !type.IsInterface)
                .Where(type => !type.IsAbstract)
                .Where(type => typeof(T).IsAssignableFrom(type));
        }
    }
}
