namespace SGAA.Api.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using SGAA.Emails;
    using SGAA.Emails.Contracts;

    public static class DependencyInjection
    {
        public static IServiceCollection AddEmails(this IServiceCollection services)
            => services
               .AddScoped<IConfirmationEmailSender, ConfirmationEmailSender>()
               .AddScoped<IAprobarUnidadEmailSender, AprobarUnidadEmailSender>()
               .AddScoped<IRechazarUnidadEmailSender, RechazarUnidadEmailSender>()
               .AddScoped<IAprobarUnidadEmailSender, AprobarUnidadEmailSender>()
               .AddScoped<IPublicarUnidadEmailSender, PublicarUnidadEmailSender>()
               .AddScoped<ICancelarPostulacionEmailSender, CancelarPostulacionEmailSender>()
               .AddScoped<IPostulacionCreadaEmailSender, PostulacionCreadaEmailSender>()
               .AddScoped<IReservaOfrecidaInquilinoEmailSender, ReservaOfrecidaInquilinoEmailSender>()
               .AddScoped<IReservaOfrecidaPropietarioEmailSender, ReservaOfrecidaPropietarioEmailSender>()
               .AddScoped<IOfertaAceptadaEmailSender, OfertaAceptadaEmailSender>()
               .AddScoped<IOfertaRechazadaEmailSender, OfertaRechazadaEmailSender>()
               .AddScoped<IAprobarAplicacionEmailSender, AprobarAplicacionEmailSender>()
               .AddScoped<IRechazarAplicacionEmailSender, RechazarAplicacionEmailSender>();
    }
}
