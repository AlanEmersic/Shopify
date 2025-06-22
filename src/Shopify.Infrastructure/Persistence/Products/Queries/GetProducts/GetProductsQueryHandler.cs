using ErrorOr;
using MediatR;
using Shopify.Application.Products.DTO;
using Shopify.Application.Products.Services;

namespace Shopify.Infrastructure.Persistence.Products.Queries.GetProducts;

internal sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ErrorOr<ProductPagedDto?>>
{
    private readonly IProductApiService productApiService;

    public GetProductsQueryHandler(IProductApiService productApiService)
    {
        this.productApiService = productApiService;
    }

    public async Task<ErrorOr<ProductPagedDto?>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        ProductPagedDto? products = await productApiService.GetProductsAsync(query.Skip, query.Limit);

        return products;
    }
}