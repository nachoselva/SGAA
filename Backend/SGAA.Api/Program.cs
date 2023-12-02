using SGAA.Api.DependencyInjection;
using SGAA.Api.Extensions;
using SGAA.Models.DependencyInjection;
using SGAA.Repository.DependencyInjection;
using SGAA.Service.DependencyInjection;
using SGAA.Utils.Configuration;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

ISGAAConfiguration configuration = new SGAAConfiguration(builder.Configuration);

builder.Services
    .AddApi(configuration)
    .AddServices()
    .AddEmails()
    .AddDocuments()
    .AddModel()
    .AddRepository();

var app = builder.Build();

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features
        .Get<IExceptionHandlerPathFeature>()!
        .Error;
    var response = new { error = exception.Message };
    await context.Response.WriteAsJsonAsync(response);
}));
app.UseRouting(); // or .UseRouting() or .UseEndpoints()

await app.MigrateDbContext();
app.UseCors("Default");

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
