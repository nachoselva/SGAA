namespace SGAA.Api.Extensions
{
    using SGAA.Repository.DependencyInjection;

    public static class ApplicationBuilder
    {
        public static async Task<WebApplication> MigrateDbContext(
            this WebApplication app)
        {
            await DependencyInjection.MigrateDbContext(app.Services);
            return app;
        }
    }
}
