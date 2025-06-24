using Shopify.Domain.Common.Models;
using Shopify.Domain.Users;

namespace Shopify.Domain.Products;

public sealed class FavoriteProduct : VersionedEntity
{
    public required int UserId { get; init; }
    public required int ProductId { get; init; }

    public User? User { get; init; }
}