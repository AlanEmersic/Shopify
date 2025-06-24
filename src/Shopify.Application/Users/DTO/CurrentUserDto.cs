namespace Shopify.Application.Users.DTO;

public sealed record CurrentUserDto(int Id, IReadOnlyList<string> Roles);