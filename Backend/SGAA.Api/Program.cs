using SGAA.Api.DependencyInjection;
using SGAA.Api.Extensions;
using SGAA.Models.DependencyInjection;
using SGAA.Repository.DependencyInjection;
using SGAA.Service.DependencyInjection;
using SGAA.Utils.Configuration;

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

await app.MigrateDbContext();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
