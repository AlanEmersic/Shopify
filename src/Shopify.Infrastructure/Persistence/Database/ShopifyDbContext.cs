using Microsoft.EntityFrameworkCore;
using Shopify.Application.Common.Interfaces;

namespace Shopify.Infrastructure.Persistence.Database;

internal sealed class ShopifyDbContext : DbContext, IUnitOfWork
{
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