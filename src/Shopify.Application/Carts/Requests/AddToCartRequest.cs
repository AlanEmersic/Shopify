namespace Shopify.Application.Carts.Requests;

public sealed record AddToCartRequest(int ProductId, int Quantity, string Title, string Thumbnail, decimal Price);