using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopify.Application.Authentication.Commands.Register;
using Shopify.Application.Authentication.DTO;

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
}