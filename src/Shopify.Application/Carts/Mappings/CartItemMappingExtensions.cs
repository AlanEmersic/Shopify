using Shopify.Application.Carts.Commands.AddToCart;
using Shopify.Application.Carts.DTO;
using Shopify.Domain.Carts;

namespace Shopify.Application.Carts.Mappings;

public static class CartItemMappingExtensions
{
    public static CartItem MapToDomain(this AddToCartCommand command, int userId)
    {
        return new CartItem { UserId = userId, ProductId = command.ProductId, Quantity = command.Quantity };
    }

    public static CartItemDto MapToDto(this CartItem cartItem)
    {
        return new CartItemDto(ProductId: cartItem.ProductId, Quantity: cartItem.Quantity);
    }
}