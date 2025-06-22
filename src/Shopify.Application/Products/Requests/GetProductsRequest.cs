namespace Shopify.Application.Products.Requests;

public sealed record GetProductsRequest(string? Search, int Skip, int Limit, string? SortBy, string? Order, string? Category);