using Microsoft.EntityFrameworkCore;
using Shopify.Application.Common.Interfaces;
using Shopify.Domain.Carts;
using Shopify.Domain.Orders;
using Shopify.Domain.Products;
using Shopify.Domain.Users;

namespace Shopify.Infrastructure.Persistence.Database;

internal sealed class ShopifyDbContext : DbContext, IUnitOfWork
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<FavoriteProduct> FavoriteProducts => Set<FavoriteProduct>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<CartItem> CartItems => Set<CartItem>();

    public ShopifyDbContext(DbContextOptions<ShopifyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopifyDbContext).Assembly);
    }

    public async Task CommitChangesAsync(CancellationToken cancellationToken = default)
    {
        await SaveChangesAsync(cancellationToken);
    }
}