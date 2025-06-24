using ErrorOr;
using MediatR;
using Shopify.Application.Authorization;
using Shopify.Domain.Users;

namespace Shopify.Application.Carts.Commands.RemoveFromCart;

[Authorize(Roles = nameof(UserRoles.Customer))]
public sealed record RemoveFromCartCommand(int ProductId) : IRequest<ErrorOr<Deleted>>;