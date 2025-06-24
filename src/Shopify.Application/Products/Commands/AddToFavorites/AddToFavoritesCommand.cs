using ErrorOr;
using MediatR;
using Shopify.Application.Authorization;
using Shopify.Domain.Users;

namespace Shopify.Application.Products.Commands.AddToFavorites;

[Authorize(Roles = nameof(UserRoles.Customer))]
public sealed record AddToFavoritesCommand(int ProductId) : IRequest<ErrorOr<Created>>;