namespace SGAA.Api.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using SGAA.Documents;
    using SGAA.Documents.Contracts;

    public static class DependencyInjection
    {
        public static IServiceCollection AddDocuments(this IServiceCollection services)
            => services
               .AddScoped<IContratoDocumentHandler, ContratoDocumentHandler>();
    }
}
