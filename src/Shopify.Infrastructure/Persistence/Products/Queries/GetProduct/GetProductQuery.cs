using ErrorOr;
using MediatR;
using Shopify.Application.Products.DTO;

namespace Shopify.Infrastructure.Persistence.Products.Queries.GetProduct;

public sealed record GetProductQuery(int Id) : IRequest<ErrorOr<ProductDto?>>;