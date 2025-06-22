using ErrorOr;
using MediatR;
using Shopify.Application.Products.DTO;
using Shopify.Application.Products.Services;

namespace Shopify.Infrastructure.Persistence.Products.Queries.GetProduct;

internal sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery, ErrorOr<ProductDto?>>
{
    private readonly IProductApiService productApiService;

    public GetProductQueryHandler(IProductApiService productApiService)
    {
        this.productApiService = productApiService;
    }

    public async Task<ErrorOr<ProductDto?>> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        ProductDto? product = await productApiService.GetProductAsync(query.Id);

        return product;
    }
}