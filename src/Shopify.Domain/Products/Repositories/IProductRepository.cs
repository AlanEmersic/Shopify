namespace Shopify.Domain.Products.Repositories;

public interface IProductRepository
{
    Task<IReadOnlyList<FavoriteProduct>> GetAllByUserIdAsync(int userId);
    Task<FavoriteProduct?> GetByUserIdAndProductIdAsync(int userId, int productId);
    Task<bool> ExistsAsync(int userId, int productId);
    Task AddAsync(FavoriteProduct favoriteProduct);
    Task DeleteAsync(FavoriteProduct favoriteProduct);
}