namespace Shopify.Application.Carts.DTO;

public sealed record CartItemDto(int ProductId, int Quantity, string Title, string Thumbnail, decimal Price);