using Shopify.Domain.Users;

namespace Shopify.Application.Users.DTO;

public record UserDto(int Id, string Email, string Address, IReadOnlyCollection<UserRoles> Roles);