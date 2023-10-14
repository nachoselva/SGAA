namespace SGAA.Models.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using SGAA.Models.Mappers;

    public static class DependencyInjection
    {
        public static IServiceCollection AddModel(this IServiceCollection services)
        {
            services.AddScoped<ICiudadMapper, CiudadMapper>();
            services.AddScoped<IProvinciaMapper, ProvinciaMapper>();
            services.AddScoped<IUsuarioMapper, UsuarioMapper>();
            services.AddScoped<IUnidadMapper, UnidadMapper>();
            services.AddScoped<IPublicacionMapper, PublicacionMapper>();
            return services;
        }
    }
}
