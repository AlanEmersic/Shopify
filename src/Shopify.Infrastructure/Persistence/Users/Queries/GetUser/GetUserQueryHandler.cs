using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopify.Application.Users.DTO;
using Shopify.Application.Users.Mappings;
using Shopify.Infrastructure.Persistence.Database;

namespace Shopify.Infrastructure.Persistence.Users.Queries.GetUser;

internal sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, ErrorOr<UserDto>>
{
    private readonly ShopifyDbContext dbContext;

    public GetUserQueryHandler(ShopifyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ErrorOr<UserDto>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        UserDto? user = await dbContext.Users
            .AsNoTracking()
            .Where(x => x.Email == query.Email)
            .Select(x => x.MapToDto())
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (user is null)
        {
            return Error.NotFound(description: "User not found");
        }

        return user;
    }
}