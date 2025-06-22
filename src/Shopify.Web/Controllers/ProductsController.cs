using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopify.Application.Products.DTO;
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
    public async Task<IActionResult> GetProducts(int skip = 0, int limit = 10)
    {
        GetProductsQuery query = new(skip, limit);
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
}