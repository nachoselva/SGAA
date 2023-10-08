namespace SGAA.Repository.DependencyInjection
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SGAA.Repository.Contexts;

    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddDbContext<SGAADbContext>();
            return services;
        }

        public static async Task MigrateDbContext(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider
                .GetRequiredService<SGAADbContext>();

            await dbContext.Database.MigrateAsync();
        }
    }
}
