using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Shopify.Application.Products.Services;
using Shopify.Domain.Common.Interfaces;
using Shopify.Infrastructure.Authentication;
using Shopify.Infrastructure.Common.Constants;
using Shopify.Infrastructure.Persistence.Database;
using Shopify.Infrastructure.Products;

namespace Shopify.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddServices();
        services.AddAuthentication(configuration);

        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddSwagger();

        services.AddProblemDetails();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        app.UseExceptionHandler();
        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

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
        services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining(typeof(InfrastructureExtensions)));

        services.AddHttpClient(ApiConstants.ClientName, client =>
        {
            const string baseApiUri = "https://dummyjson.com";
            client.BaseAddress = new Uri(baseApiUri);
        });

        services.AddScoped<IProductApiService, ProductApiService>();
    }

    private static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
    }

    private static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Shopify API", Version = "v1" });
        });
    }
}