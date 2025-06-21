using ErrorOr;

namespace Shopify.Infrastructure.Authentication.Errors;

internal static class PasswordHasherErrors
{
    public static readonly Error PasswordTooWeak = Error.Validation(
        code: PasswordHasherCodes.PasswordTooWeak,
        description: "Password too weak.");
}