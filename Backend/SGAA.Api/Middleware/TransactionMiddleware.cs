namespace SGAA.Api.Middleware
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.EntityFrameworkCore.Storage;
    using SGAA.Repository.Contexts;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class TransactionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Endpoint? endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            var transactionalAttribute = endpoint?.Metadata?.GetMetadata<TransactionalAttribute>();

            if(transactionalAttribute == null)
            {
                await next(context);
            }
            else
            {
                SGAADbContext dbContext = context.RequestServices.GetRequiredService<SGAADbContext>();

                IDbContextTransaction? transaction = null;
                try
                {
                    transaction = await dbContext.Database.BeginTransactionAsync();

                    await next(context);

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    if (transaction != null)
                        await transaction.RollbackAsync();

                    throw;
                }
                finally
                {
                    if (transaction != null)
                        await transaction.DisposeAsync();
                }
            }
        }
    }
}
