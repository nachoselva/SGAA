namespace SGAA.Api.Extensions
{
    using SGAA.Api.Middleware;
    using SGAA.Repository.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static async Task<WebApplication> MigrateDbContext(
            this WebApplication app)
        {
            await DependencyInjection.MigrateDbContext(app.Services);
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<TransactionMiddleware>();
            return app;
        }
    }
}
