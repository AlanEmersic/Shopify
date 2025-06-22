using MediatR;
using Shopify.Application.Products.DTO;
using Shopify.Application.Products.Services;

namespace Shopify.Infrastructure.Persistence.Products.Queries.GetCategories;

internal sealed class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IReadOnlyList<CategoryDto>?>
{
    private readonly IProductApiService productApiService;

    public GetCategoriesQueryHandler(IProductApiService productApiService)
    {
        this.productApiService = productApiService;
    }

    public async Task<IReadOnlyList<CategoryDto>?> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<CategoryDto>? categories = await productApiService.GetCategoriesAsync(cancellationToken);

        return categories;
    }
}