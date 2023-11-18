namespace SGAA.Api.DependencyInjection
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using SGAA.Api.Middleware;
    using SGAA.Api.Providers;
    using SGAA.Domain.Auth;
    using SGAA.Repository.Contexts;
    using SGAA.Utils.Configuration;
    using System.Text;
    using System.Text.Json.Serialization;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services, ISGAAConfiguration configuration)
        {
            services.AddControllers()
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            services.AddEndpointsApiExplorer();
            services.AddCors(options =>
            {
                options.AddPolicy("Default",
                    builder =>
                    {
                        builder
                        .WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "SGAA API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
                });
            });
            services.AddAuthorization();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = configuration.Jwt.Audience,
                    ValidIssuer = configuration.Jwt.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Jwt.Key))
                };
            });
            services.AddHttpContextAccessor();
            services.AddScoped<ExceptionMiddleware>();
            services.AddScoped<TransactionMiddleware>();
            services.AddScoped<IUsuarioProvider, UsuarioProvider>();
            services.AddSingleton<ISGAAConfiguration, SGAAConfiguration>();
            services.AddIdentityCore<Usuario>(
                options =>
                    {
                        options.Password.RequiredLength = 7;
                        options.Password.RequireDigit = false;
                        options.Password.RequireUppercase = false;
                        options.User.RequireUniqueEmail = true;
                        options.SignIn.RequireConfirmedAccount = true;
                    }
                )
                .AddTokenProvider<DataProtectorTokenProvider<Usuario>>(TokenOptions.DefaultProvider)
                .AddRoles<Rol>()
                .AddEntityFrameworkStores<SGAADbContext>();
            return services;
        }
    }
}
