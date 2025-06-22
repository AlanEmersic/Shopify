using Shopify.Domain.Users;

namespace Shopify.Domain.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}