namespace SGAA.Service.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using SGAA.Service.Contracts;

    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
            => services
                .AddScoped<ICiudadService, CiudadService>()
                .AddScoped<IProvinciaService, ProvinciaService>()
                .AddScoped<ISecurityService, UsuarioService>()
                .AddScoped<IUsuarioService, UsuarioService>()
                .AddScoped<IUnidadService, UnidadService>()
                .AddScoped<IPublicacionService, PublicacionService>()
                .AddScoped<IPostulacionService, PostulacionService>()
                .AddScoped<IAplicacionService, AplicacionService>();
    }
}
