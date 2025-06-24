using Microsoft.EntityFrameworkCore;
using Shopify.Domain.Products;
using Shopify.Domain.Products.Repositories;
using Shopify.Infrastructure.Persistence.Database;

namespace Shopify.Infrastructure.Persistence.Products.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly ShopifyDbContext dbContext;

    public ProductRepository(ShopifyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IReadOnlyList<FavoriteProduct>> GetAllByUserIdAsync(int userId)
    {
        return await dbContext.FavoriteProducts.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<FavoriteProduct?> GetByUserIdAndProductIdAsync(int userId, int productId)
    {
        return await dbContext.FavoriteProducts.AsNoTracking().Where(x => x.UserId == userId && x.ProductId == productId).FirstOrDefaultAsync();
    }

    public async Task<bool> ExistsAsync(int userId, int productId)
    {
        return await dbContext.FavoriteProducts.AnyAsync(x => x.UserId == userId && x.ProductId == productId);
    }

    public async Task AddAsync(FavoriteProduct favoriteProduct)
    {
        dbContext.FavoriteProducts.Add(favoriteProduct);

        await Task.CompletedTask;
    }

    public async Task DeleteAsync(FavoriteProduct favoriteProduct)
    {
        dbContext.FavoriteProducts.Remove(favoriteProduct);

        await Task.CompletedTask;
    }
}