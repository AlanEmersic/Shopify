namespace Shopify.Application.Carts.DTO;

public sealed record CartDto(IReadOnlyList<CartItemDto> CartItems);