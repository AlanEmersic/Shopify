using Shopify.Domain.Common;

namespace Shopify.Domain.Orders;

public sealed class Order : VersionedEntity
{
    public required int UserId { get; init; }
    public OrderStatus Status { get; private set; } = OrderStatus.Draft;
    public decimal TotalAmount => orderItems.Sum(x => x.TotalPrice);

    public IReadOnlyCollection<OrderItem> OrderItems => orderItems.AsReadOnly();
    private readonly List<OrderItem> orderItems = [];
}