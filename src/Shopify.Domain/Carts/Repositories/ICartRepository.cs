namespace Shopify.Domain.Carts.Repositories;

public interface ICartRepository
{
    Task<IReadOnlyList<CartItem>> GetAllByUserIdAsync(int userId);
    Task<CartItem?> GetByUserIdAndProductIdAsync(int userId, int productId);
    Task AddAsync(CartItem cartItem);
    Task UpdateAsync(CartItem cartItem);
    Task DeleteAsync(CartItem cartItem);
}