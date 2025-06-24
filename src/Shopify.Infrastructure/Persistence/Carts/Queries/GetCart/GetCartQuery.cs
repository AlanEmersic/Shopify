using ErrorOr;
using MediatR;
using Shopify.Application.Authorization;
using Shopify.Application.Carts.DTO;
using Shopify.Domain.Users;

namespace Shopify.Infrastructure.Persistence.Carts.Queries.GetCart;

[Authorize(Roles = nameof(UserRoles.Customer))]
public sealed record GetCartQuery : IRequest<ErrorOr<CartDto>>;