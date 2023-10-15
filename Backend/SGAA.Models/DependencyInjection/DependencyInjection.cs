namespace SGAA.Models.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using SGAA.Models.Mappers;

    public static class DependencyInjection
    {
        public static IServiceCollection AddModel(this IServiceCollection services)
            => services
                .AddScoped<ICiudadMapper, CiudadMapper>()
                .AddScoped<IProvinciaMapper, ProvinciaMapper>()
                .AddScoped<IUsuarioMapper, UsuarioMapper>()
                .AddScoped<IUnidadMapper, UnidadMapper>()
                .AddScoped<IPublicacionMapper, PublicacionMapper>()
                .AddScoped<IPostulacionMapper, PostulacionMapper>()
                .AddScoped<IAplicacionMapper, AplicacionMapper>();
    }
}
