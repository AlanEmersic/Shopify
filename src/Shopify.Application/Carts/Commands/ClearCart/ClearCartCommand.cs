using ErrorOr;
using MediatR;
using Shopify.Application.Authorization;
using Shopify.Domain.Users;

namespace Shopify.Application.Carts.Commands.ClearCart;

[Authorize(Roles = nameof(UserRoles.Customer))]
public sealed record ClearCartCommand() : IRequest<ErrorOr<Deleted>>;