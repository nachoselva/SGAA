namespace SGAA.Repository.DependencyInjection
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SGAA.Repository.Contexts;
    using SGAA.Repository.Contracts;

    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
            => services
                .AddDbContext<SGAADbContext>()
                .AddScoped<ICiudadRepository, CiudadRepository>()
                .AddScoped<IProvinciaRepository, ProvinciaRepository>()
                .AddScoped<IUsuarioRepository, UsuarioRepository>()
                .AddScoped<IUnidadRepository, UnidadRepository>()
                .AddScoped<IPublicacionRepository, PublicacionRepository>()
                .AddScoped<IPostulacionRepository, PostulacionRepository>()
                .AddScoped<IAplicacionRepository, AplicacionRepository>();

        public static async Task MigrateDbContext(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider
                .GetRequiredService<SGAADbContext>();

            await dbContext.Database.MigrateAsync();
        }
    }
}
