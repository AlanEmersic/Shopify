namespace Shopify.Domain.Carts.Repositories;

public interface ICartRepository
{
    Task<CartItem?> GetByUserIdAndProductIdAsync(int userId, int productId);
    Task AddAsync(CartItem cartItem);
    Task UpdateAsync(CartItem cartItem);
    Task DeleteAsync(CartItem cartItem);
    Task DeleteAllByUserIdAsync(int userId);
}