using Microsoft.Extensions.Logging;
using Shopify.Application.Products.DTO;
using Shopify.Application.Products.Services;
using Shopify.Infrastructure.Common.Constants;
using System.Net.Http.Json;

namespace Shopify.Infrastructure.Products;

internal sealed class ProductApiService : IProductApiService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ILogger<ProductApiService> logger;

    public ProductApiService(IHttpClientFactory httpClientFactory, ILogger<ProductApiService> logger)
    {
        this.httpClientFactory = httpClientFactory;
        this.logger = logger;
    }

    public async Task<ProductPagedDto?> GetProductsAsync(int skip, int limit)
    {
        try
        {
            string requestUri = $"products?skip={skip}&limit={limit}";
            HttpClient httpClient = httpClientFactory.CreateClient(ApiConstants.ClientName);
            ProductPagedDto? response = await httpClient.GetFromJsonAsync<ProductPagedDto?>(requestUri);

            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            return null;
        }
    }
}