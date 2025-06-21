using ErrorOr;
using MediatR;
using Shopify.Application.Products.DTO;

namespace Shopify.Infrastructure.Persistence.Products.Queries.GetProducts;

public sealed record GetProductsQuery(int Skip, int Limit) : IRequest<ErrorOr<ProductPagedDto?>>;