using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Shopify.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();

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
    }

    private static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Shopify API", Version = "v1" });
        });
    }
}