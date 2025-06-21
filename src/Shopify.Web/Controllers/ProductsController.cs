using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Shopify.Application.Products.DTO;
using Shopify.Application.Products.Services;

namespace Shopify.Web.Controllers;

[Route("api/[controller]")]
public sealed class ProductsController : ApiController
{
    private readonly IProductApiService productApiService;

    public ProductsController(IProductApiService productApiService)
    {
        this.productApiService = productApiService;
    }

    [HttpGet]
    public async Task<IActionResult> GetSettlements(int skip = 1, int limit = 10)
    {
        ErrorOr<ProductPagedDto?> result = await productApiService.GetProductsAsync(skip, limit);

        return result.Match(Ok, Problem);
    }
}