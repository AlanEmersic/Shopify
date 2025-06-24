using Shopify.Application.Products.Commands.AddToFavorites;
using Shopify.Domain.Products;

namespace Shopify.Application.Products.Mappings;

public static class ProductMappingExtensions
{
    public static FavoriteProduct MapToDomain(this AddToFavoritesCommand command, int userId)
    {
        return new FavoriteProduct { UserId = userId, ProductId = command.ProductId };
    }
}