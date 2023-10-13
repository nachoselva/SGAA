namespace SGAA.Api.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using SGAA.Emails;

    public static class DependencyInjection
    {
        public static IServiceCollection AddEmails(this IServiceCollection services)
        {
            services.AddScoped<IConfirmationEmailSender, ConfirmationEmailSender>();
            return services;
        }
    }
}
