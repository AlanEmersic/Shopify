using Shopify.Application.Users.DTO;
using Shopify.Domain.Users;

namespace Shopify.Application.Users.Mappings;

public static class UserMappingExtensions
{
    public static UserDto MapToDto(this User user)
    {
        return new UserDto(Id: user.Id, Email: user.Email, Address: user.Address, Roles: user.Roles);
    }
}