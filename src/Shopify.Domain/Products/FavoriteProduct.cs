using Shopify.Domain.Common;
using Shopify.Domain.Users;

namespace Shopify.Domain.Products;

public sealed class FavoriteProduct : Entity
{
    public required int UserId { get; init; }
    public required int ProductId { get; init; }

    public User? User { get; init; }
    public Product? Product { get; init; }
}