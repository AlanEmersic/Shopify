using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopify.Application.Authorization;
using Shopify.Application.Carts.Commands.AddToCart;
using Shopify.Application.Carts.Commands.ClearCart;
using Shopify.Application.Carts.Commands.RemoveFromCart;
using Shopify.Application.Carts.DTO;
using Shopify.Application.Carts.Requests;
using Shopify.Domain.Users;
using Shopify.Infrastructure.Persistence.Carts.Queries.GetCart;

namespace Shopify.Web.Controllers;

[Route("api/[controller]")]
public sealed class CartsController : ApiController
{
    private readonly ISender mediator;

    public CartsController(ISender mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = nameof(UserRoles.Customer))]
    public async Task<IActionResult> GetCart()
    {
        GetCartQuery query = new();
        ErrorOr<CartDto> result = await mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpPost]
    [Authorize(Roles = nameof(UserRoles.Customer))]
    public async Task<IActionResult> AddToCart(AddToCartRequest request)
    {
        AddToCartCommand command = new(request.ProductId, request.Quantity, request.Title, request.Thumbnail, request.Price);
        ErrorOr<Created> result = await mediator.Send(command);

        return result.Match(_ => CreatedAtAction(nameof(AddToCart), default), Problem);
    }

    [HttpDelete("{productId:int}")]
    [Authorize(Roles = nameof(UserRoles.Customer))]
    public async Task<IActionResult> RemoveFromCart(int productId)
    {
        RemoveFromCartCommand command = new(productId);
        ErrorOr<Deleted> result = await mediator.Send(command);

        return result.Match(_ => NoContent(), Problem);
    }

    [HttpDelete("clear")]
    [Authorize(Roles = nameof(UserRoles.Customer))]
    public async Task<IActionResult> ClearCart()
    {
        ClearCartCommand command = new();
        ErrorOr<Deleted> result = await mediator.Send(command);

        return result.Match(_ => NoContent(), Problem);
    }
}