using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopify.Application.Users.DTO;
using Shopify.Domain.Users;
using Shopify.Infrastructure.Persistence.Users.Queries.GetUser;

namespace Shopify.Web.Controllers;

[Route("api/[controller]")]
public sealed class UsersController : ApiController
{
    private readonly ISender mediator;

    public UsersController(ISender mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{email}")]
    [Authorize(Roles = nameof(UserRoles.Customer))]
    public async Task<IActionResult> GetUser(string email)
    {
        GetUserQuery query = new(email);
        ErrorOr<UserDto> result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }
}