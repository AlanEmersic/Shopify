using Microsoft.AspNetCore.Http;
using Shopify.Application.Users.DTO;
using Shopify.Application.Users.Services;
using System.Security.Claims;

namespace Shopify.Infrastructure.Persistence.Users.Services;

internal class CurrentUserProvider : ICurrentUserProvider
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public CurrentUserDto GetCurrentUser()
    {
        if (httpContextAccessor.HttpContext is null)
        {
            throw new InvalidOperationException("HttpContext is null");
        }

        const string idClaim = "id";
        int id = GetClaimValues(idClaim).Select(int.Parse).FirstOrDefault();

        IReadOnlyList<string> roles = GetClaimValues(ClaimTypes.Role);

        return new CurrentUserDto(id, roles);
    }

    private IReadOnlyList<string> GetClaimValues(string claimType)
    {
        return httpContextAccessor.HttpContext!.User.Claims
            .Where(claim => claim.Type == claimType)
            .Select(claim => claim.Value)
            .ToList();
    }
}