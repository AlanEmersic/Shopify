using ErrorOr;
using MediatR;
using Shopify.Application.Authentication.DTO;

namespace Shopify.Infrastructure.Authentication.Queries;

public sealed record LoginQuery(string Email, string Password) : IRequest<ErrorOr<AuthenticationDto>>;