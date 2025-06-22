using Shopify.Application.Authentication.Commands.Register;
using Shopify.Domain.Users;

namespace Shopify.Application.Authentication.Mappings;

internal static class AuthenticationMappingExtensions
{
    public static User MapToDomain(this RegisterCommand command, string hashPassword)
    {
        return new User { Email = command.Email, Password = hashPassword, Address = command.Address, Roles = new List<UserRoles> { UserRoles.Customer } };
    }
}