using ErrorOr;
using MediatR;
using Shopify.Application.Authentication.DTO;

namespace Shopify.Application.Authentication.Commands.Register;

public sealed record RegisterCommand(string Email, string Password, string Address) : IRequest<ErrorOr<AuthenticationDto>>;