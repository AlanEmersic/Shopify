using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Shopify.Infrastructure.Persistence.Database;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        ShopifyDbContext dbContext = scope.ServiceProvider.GetRequiredService<ShopifyDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}