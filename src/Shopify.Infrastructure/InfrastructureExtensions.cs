using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Shopify.Application.Products.Services;
using Shopify.Application.Users.Services;
using Shopify.Domain.Common.Interfaces;
using Shopify.Infrastructure.Authentication;
using Shopify.Infrastructure.Authentication.Filters;
using Shopify.Infrastructure.Authentication.JwtToken;
using Shopify.Infrastructure.Authorization.Behaviors;
using Shopify.Infrastructure.Common.Constants;
using Shopify.Infrastructure.Persistence.Database;
using Shopify.Infrastructure.Persistence.Products.Services;
using Shopify.Infrastructure.Persistence.Users.Services;
using System.Text;
using System.Text.Json.Serialization;

namespace Shopify.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostBuilder hostBuilder)
    {
        services.AddDatabase(configuration);
        services.AddServices();
        services.AddAuthentication(configuration);
        services.AddMemoryCache();

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        services.AddEndpointsApiExplorer();

        services.AddSwagger();

        services.AddProblemDetails();
        services.AddHttpContextAccessor();

        hostBuilder.UseSerilog((_, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(configuration));

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:3000"));
        app.UseExceptionHandler();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSerilogRequestLogging();

        if (environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseEndpoints(builder =>
        {
            builder.MapControllers();
        });

        return app;
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(InfrastructureExtensions));
            options.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
        });

        services.AddHttpClient(ApiConstants.ClientName, client =>
        {
            const string baseApiUri = "https://dummyjson.com";
            client.BaseAddress = new Uri(baseApiUri);
        });

        services.AddScoped<IProductApiService, ProductApiService>();
        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
    }

    private static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        JwtSettings jwtSettings = new();
        configuration.Bind(JwtSettings.Section, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services
            .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });
    }

    private static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Shopify API", Version = "v1" });

            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme { Type = SecuritySchemeType.Http, Scheme = JwtBearerDefaults.AuthenticationScheme });

            options.OperationFilter<AuthenticationRequirementsOperationFilter>();
        });
    }
}