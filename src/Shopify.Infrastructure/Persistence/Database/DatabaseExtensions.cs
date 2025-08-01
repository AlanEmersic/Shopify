﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shopify.Application.Common.Interfaces;
using Shopify.Domain.Carts.Repositories;
using Shopify.Domain.Products.Repositories;
using Shopify.Domain.Users.Repositories;
using Shopify.Infrastructure.Persistence.Carts.Repositories;
using Shopify.Infrastructure.Persistence.Products.Repositories;
using Shopify.Infrastructure.Persistence.Users.Repositories;

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

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ShopifyDbContext>());
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICartRepository, CartRepository>();

        return services;
    }
}