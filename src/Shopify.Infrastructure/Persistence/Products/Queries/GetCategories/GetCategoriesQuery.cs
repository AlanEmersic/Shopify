using MediatR;
using Shopify.Application.Products.DTO;

namespace Shopify.Infrastructure.Persistence.Products.Queries.GetCategories;

public sealed record GetCategoriesQuery : IRequest<IReadOnlyList<CategoryDto>?>;