using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopify.Application.Authentication.Commands.Register;
using Shopify.Application.Authentication.DTO;
using Shopify.Application.Authentication.Requests;
using Shopify.Infrastructure.Authentication.Errors;
using Shopify.Infrastructure.Authentication.Queries;

namespace Shopify.Web.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
public sealed class AuthenticationController : ApiController
{
    private readonly ISender mediator;

    public AuthenticationController(ISender mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        ErrorOr<AuthenticationDto> authenticationResult = await mediator.Send(command);

        return authenticationResult.Match(Ok, Problem);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        LoginQuery query = new(request.Email, request.Password);
        ErrorOr<AuthenticationDto> authenticationResult = await mediator.Send(query);

        if (authenticationResult.IsError && authenticationResult.FirstError == AuthenticationErrors.InvalidCredentials)
        {
            return Problem(detail: authenticationResult.FirstError.Description, statusCode: StatusCodes.Status401Unauthorized);
        }

        return authenticationResult.Match(Ok, Problem);
    }
}