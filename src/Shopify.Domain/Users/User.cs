using Shopify.Domain.Common.Models;
using Shopify.Domain.Orders;

namespace Shopify.Domain.Users;

public sealed class User : VersionedEntity
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string Address { get; init; }

    public required IReadOnlyCollection<UserRoles> Roles { get; init; }
    public IReadOnlyCollection<Order> Orders { get; init; } = [];
}