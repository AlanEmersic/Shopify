using Shopify.Domain.Common;

namespace Shopify.Domain.Products;

public sealed class Review : VersionedEntity
{
    public required int Rating { get; init; }
    public required string Comment { get; init; }
    public required string ReviewerName { get; init; }
    public required string ReviewerEmail { get; init; }
    public required DateTime Date { get; init; }
}