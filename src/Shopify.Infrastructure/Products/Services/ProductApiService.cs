using Microsoft.Extensions.Logging;
using Shopify.Application.Products.DTO;
using Shopify.Application.Products.Services;
using Shopify.Infrastructure.Common.Constants;
using System.Net.Http.Json;

namespace Shopify.Infrastructure.Products.Services;

internal sealed class ProductApiService : IProductApiService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ILogger<ProductApiService> logger;

    public ProductApiService(IHttpClientFactory httpClientFactory, ILogger<ProductApiService> logger)
    {
        this.httpClientFactory = httpClientFactory;
        this.logger = logger;
    }

    public async Task<ProductPagedDto?> GetProductsAsync(string? search, int skip, int limit, string? sortBy, string? order, string? category, CancellationToken cancellationToken)
    {
        try
        {
            string requestUri = $"products?skip={skip}&limit={limit}";

            if (!string.IsNullOrWhiteSpace(category))
            {
                requestUri = $"products/category/{category}?skip={skip}&limit={limit}";
            }
            else if (!string.IsNullOrWhiteSpace(search))
            {
                requestUri = $"products/search?q={search}&skip={skip}&limit={limit}";
            }

            List<string> queryParams = new();

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                queryParams.Add($"sortBy={sortBy}");
            }

            if (!string.IsNullOrWhiteSpace(order))
            {
                queryParams.Add($"order={order}");
            }

            if (queryParams.Count > 0)
            {
                requestUri += "&" + string.Join("&", queryParams);
            }

            HttpClient httpClient = httpClientFactory.CreateClient(ApiConstants.ClientName);
            ProductPagedDto? response = await httpClient.GetFromJsonAsync<ProductPagedDto?>(requestUri, cancellationToken);

            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            return null;
        }
    }

    public async Task<ProductDto?> GetProductAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            string requestUri = $"products/{id}";
            HttpClient httpClient = httpClientFactory.CreateClient(ApiConstants.ClientName);
            ProductDto? response = await httpClient.GetFromJsonAsync<ProductDto?>(requestUri, cancellationToken);

            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            return null;
        }
    }

    public async Task<IReadOnlyList<CategoryDto>?> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        try
        {
            string requestUri = "products/categories";
            HttpClient httpClient = httpClientFactory.CreateClient(ApiConstants.ClientName);
            IReadOnlyList<CategoryDto>? response = await httpClient.GetFromJsonAsync<IReadOnlyList<CategoryDto>?>(requestUri, cancellationToken);

            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            return null;
        }
    }
}