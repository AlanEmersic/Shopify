using Shopify.Domain.Common.Models;

namespace Shopify.Domain.Products;

public sealed class Product : VersionedEntity
{
    public required string ExternalId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required string Category { get; init; }
    public required string Brand { get; init; }
    public required string Sku { get; init; }
    public required decimal Price { get; init; }
    public decimal? DiscountPercentage { get; init; }
    public float Rating { get; init; }
    public int Stock { get; init; }
    public int MinimumOrderQuantity { get; init; }
    public required Dimensions Dimensions { get; init; }
    public required int Weight { get; init; }
    public string? WarrantyInformation { get; init; }
    public string? ShippingInformation { get; init; }
    public string? AvailabilityStatus { get; init; }
    public string? ReturnPolicy { get; init; }
    public required string Thumbnail { get; init; }
    public required ProductMeta Meta { get; init; }
    public required IReadOnlyCollection<string> Images { get; init; }
    public required IReadOnlyCollection<string> Tags { get; init; }
    public IReadOnlyCollection<Review> Reviews { get; init; } = new List<Review>();
}