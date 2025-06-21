using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shopify.Application.Common.Interfaces;

namespace Shopify.Infrastructure.Persistence.Database;

internal static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        const string sectionName = "Database";
        string? connectionString = configuration.GetConnectionString(sectionName);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Database connection string not found");
        }

        services.AddDbContext<ShopifyDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddHostedService<DatabaseInitializer>();

        services.AddScoped<IUnitOfWork>(serviceProvider =>
            serviceProvider.GetRequiredService<ShopifyDbContext>()
        );

        return services;
    }
}