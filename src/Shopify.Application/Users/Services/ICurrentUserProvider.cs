using Shopify.Application.Users.DTO;

namespace Shopify.Application.Users.Services;

public interface ICurrentUserProvider
{
    CurrentUserDto GetCurrentUser();
}