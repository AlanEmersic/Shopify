using Microsoft.EntityFrameworkCore;
using Shopify.Domain.Carts;
using Shopify.Domain.Carts.Repositories;
using Shopify.Infrastructure.Persistence.Database;

namespace Shopify.Infrastructure.Persistence.Carts.Repositories;

internal sealed class CartRepository : ICartRepository
{
    private readonly ShopifyDbContext dbContext;

    public CartRepository(ShopifyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IReadOnlyList<CartItem>> GetAllByUserIdAsync(int userId)
    {
        return await dbContext.CartItems.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<CartItem?> GetByUserIdAndProductIdAsync(int userId, int productId)
    {
        return await dbContext.CartItems.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);
    }

    public async Task AddAsync(CartItem cartItem)
    {
        dbContext.CartItems.Add(cartItem);

        await Task.CompletedTask;
    }

    public async Task UpdateAsync(CartItem cartItem)
    {
        dbContext.CartItems.Update(cartItem);

        await Task.CompletedTask;
    }

    public async Task DeleteAsync(CartItem cartItem)
    {
        dbContext.CartItems.Remove(cartItem);

        await Task.CompletedTask;
    }
}