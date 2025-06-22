using ErrorOr;
using MediatR;
using Shopify.Application.Products.DTO;

namespace Shopify.Infrastructure.Persistence.Products.Queries.GetProducts;

public sealed record GetProductsQuery(string? Search, int Skip, int Limit, string? SortBy, string? Order, string? Category) : IRequest<ErrorOr<ProductPagedDto?>>;