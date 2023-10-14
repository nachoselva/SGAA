namespace SGAA.Api.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using SGAA.Emails;
    using SGAA.Emails.Contracts;

    public static class DependencyInjection
    {
        public static IServiceCollection AddEmails(this IServiceCollection services)
        {
            services.AddScoped<IConfirmationEmailSender, ConfirmationEmailSender>();
            services.AddScoped<IAprobarUnidadEmailSender, AprobarUnidadEmailSender>();
            services.AddScoped<IRechazarUnidadEmailSender, RechazarUnidadEmailSender>();
            return services;
        }
    }
}
