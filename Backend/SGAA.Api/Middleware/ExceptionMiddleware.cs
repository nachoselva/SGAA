namespace SGAA.Api.Middleware
{
    using Microsoft.AspNetCore.Http;
    using SGAA.Domain.Errors;
    using System;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                switch (exception)
                {
                    case BadRequestException:
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.Response.WriteAsync(JsonSerializer.Serialize((exception as BadRequestException)!.GetValidationErrors()));
                        break;
                    case UnauthorizedException:
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case NotFoundException:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ForbiddenException:
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        break;
                    case InternalServerErrorException:
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
            }
        }
    }
}
