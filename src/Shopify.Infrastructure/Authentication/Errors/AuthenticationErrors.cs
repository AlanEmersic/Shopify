using ErrorOr;

namespace Shopify.Infrastructure.Authentication.Errors;

public static class AuthenticationErrors
{
    public static readonly Error PasswordTooWeak = Error.Validation(
        code: AuthenticationCodes.PasswordTooWeak,
        description: "Password too weak.");

    public static readonly Error InvalidCredentials = Error.Validation(
        code: AuthenticationCodes.InvalidCredentials,
        description: "Invalid credentials.");
}