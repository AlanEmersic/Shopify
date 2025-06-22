using Shopify.Application.Products.DTO;

namespace Shopify.Application.Products.Services;

public interface IProductApiService
{
    Task<ProductPagedDto?> GetProductsAsync(string? search, int skip, int limit, string? sortBy, string? order, string? category, CancellationToken cancellationToken = default);
    Task<ProductDto?> GetProductAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CategoryDto>?> GetCategoriesAsync(CancellationToken cancellationToken = default);
}