using Shopify.Application.Products.DTO;

namespace Shopify.Application.Products.Services;

public interface IProductApiService
{
    Task<ProductPagedDto?> GetProductsAsync(int skip, int limit);
}