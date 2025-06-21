namespace Shopify.Application.Products.DTO;

public sealed record ProductPagedDto(IReadOnlyList<ProductDto> Products, int Total, int Skip, int Limit);