namespace SGAA.Service.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using SGAA.Service.Contracts;

    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICiudadService, CiudadService>();
            services.AddScoped<IProvinciaService, ProvinciaService>();
            services.AddScoped<ISecurityService, UsuarioService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IUnidadService, UnidadService>();
            return services;
        }
    }
}
