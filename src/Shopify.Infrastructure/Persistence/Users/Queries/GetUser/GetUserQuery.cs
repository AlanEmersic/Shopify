using ErrorOr;
using MediatR;
using Shopify.Application.Authorization;
using Shopify.Application.Users.DTO;
using Shopify.Domain.Users;

namespace Shopify.Infrastructure.Persistence.Users.Queries.GetUser;

[Authorize(Roles = nameof(UserRoles.Customer))]
public sealed record GetUserQuery(string Email) : IRequest<ErrorOr<UserDto>>;