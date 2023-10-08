namespace SGAA.Repository
{
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddDbContext<SGAADbContext>();
            return services;
        }
    }
}
