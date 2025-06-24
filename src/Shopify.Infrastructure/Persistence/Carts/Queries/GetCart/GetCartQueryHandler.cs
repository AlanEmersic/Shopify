using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopify.Application.Carts.DTO;
using Shopify.Application.Carts.Mappings;
using Shopify.Application.Users.DTO;
using Shopify.Application.Users.Services;
using Shopify.Infrastructure.Persistence.Database;

namespace Shopify.Infrastructure.Persistence.Carts.Queries.GetCart;

internal sealed class GetCartQueryHandler : IRequestHandler<GetCartQuery, ErrorOr<CartDto>>
{
    private readonly ShopifyDbContext dbContext;
    private readonly ICurrentUserProvider currentUserProvider;

    public GetCartQueryHandler(ShopifyDbContext dbContext, ICurrentUserProvider currentUserProvider)
    {
        this.dbContext = dbContext;
        this.currentUserProvider = currentUserProvider;
    }

    public async Task<ErrorOr<CartDto>> Handle(GetCartQuery query, CancellationToken cancellationToken)
    {
        CurrentUserDto user = currentUserProvider.GetCurrentUser();

        List<CartItemDto> cart = await dbContext.CartItems
            .AsNoTracking()
            .Where(x => x.UserId == user.Id)
            .Select(x => x.MapToDto())
            .ToListAsync(cancellationToken);

        return new CartDto(cart.AsReadOnly());
    }
}