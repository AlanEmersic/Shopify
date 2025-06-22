using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopify.Application.Products.DTO;
using Shopify.Application.Products.Requests;
using Shopify.Infrastructure.Persistence.Products.Queries.GetCategories;
using Shopify.Infrastructure.Persistence.Products.Queries.GetProduct;
using Shopify.Infrastructure.Persistence.Products.Queries.GetProducts;

namespace Shopify.Web.Controllers;

[Route("api/[controller]")]
public sealed class ProductsController : ApiController
{
    private readonly ISender mediator;

    public ProductsController(ISender mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] GetProductsRequest request)
    {
        GetProductsQuery query = new(request.Search, request.Skip, request.Limit, request.SortBy, request.Order, request.Category);
        ErrorOr<ProductPagedDto?> result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        GetProductQuery query = new(id);
        ErrorOr<ProductDto?> result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories()
    {
        GetCategoriesQuery query = new();
        IReadOnlyList<CategoryDto>? result = await mediator.Send(query);

        return Ok(result);
    }
}