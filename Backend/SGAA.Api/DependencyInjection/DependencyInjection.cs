namespace SGAA.Api.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using SGAA.Api.Middleware;
    using SGAA.Domain.Auth;
    using SGAA.Repository.Contexts;
    using SGAA.Utils.Configuration;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddTransient<ExceptionMiddleware>();
            services.AddSingleton<ISGAAConfiguration, SGAAConfiguration>();
            services.AddIdentity<Usuario, Rol>().AddEntityFrameworkStores<SGAADbContext>();
            return services;
        }
    }
}
