using Shopify.Domain.Common;

namespace Shopify.Domain.Products;

public sealed class ProductMeta : ValueObject
{
    public required DateTime CreatedAt { get; init; }
    public required DateTime UpdatedAt { get; init; }
    public required string Barcode { get; init; }
    public required string QrCodeUrl { get; init; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return CreatedAt;
        yield return UpdatedAt;
        yield return Barcode;
        yield return QrCodeUrl;
    }
}