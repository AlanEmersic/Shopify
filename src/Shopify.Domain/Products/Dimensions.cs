using Shopify.Domain.Common;

namespace Shopify.Domain.Products;

public sealed class Dimensions : ValueObject
{
    public required float Width { get; init; }
    public required float Height { get; init; }
    public required float Depth { get; init; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Width;
        yield return Height;
        yield return Depth;
    }
}