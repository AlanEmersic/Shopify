using ErrorOr;
using MediatR;
using Shopify.Application.Authorization;
using Shopify.Domain.Users;

namespace Shopify.Application.Products.Commands.RemoveFromFavorites;

[Authorize(Roles = nameof(UserRoles.Customer))]
public sealed record RemoveFromFavoritesCommand(int ProductId) : IRequest<ErrorOr<Deleted>>;