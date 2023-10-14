﻿namespace SGAA.Repository.DependencyInjection
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SGAA.Repository.Contexts;
    using SGAA.Repository.Contracts;

    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddDbContext<SGAADbContext>();
            services.AddScoped<ICiudadRepository, CiudadRepository>();
            services.AddScoped<IProvinciaRepository, ProvinciaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUnidadRepository, UnidadRepository>();
            services.AddScoped<IPublicacionRepository, PublicacionRepository>();
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
