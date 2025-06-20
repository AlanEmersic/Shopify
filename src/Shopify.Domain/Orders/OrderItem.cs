using Shopify.Domain.Common;
using Shopify.Domain.Products;

namespace Shopify.Domain.Orders;

public sealed class OrderItem : Entity
{
    public required int OrderId { get; init; }
    public required int ProductId { get; init; }
    public required int Quantity { get; set; }
    public required decimal UnitPrice { get; init; }
    public decimal TotalPrice => UnitPrice * Quantity;

    public Order? Order { get; init; }
    public Product? Product { get; init; }
}