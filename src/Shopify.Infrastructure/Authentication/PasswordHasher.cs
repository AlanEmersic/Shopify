using ErrorOr;
using Shopify.Domain.Common.Interfaces;
using Shopify.Infrastructure.Authentication.Errors;
using System.Text.RegularExpressions;

namespace Shopify.Infrastructure.Authentication;

internal partial class PasswordHasher : IPasswordHasher
{
    [GeneratedRegex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", RegexOptions.Compiled)]
    private static partial Regex StrongPasswordRegex();

    private static readonly Regex PasswordRegex = StrongPasswordRegex();

    public ErrorOr<string> HashPassword(string password)
    {
        return !PasswordRegex.IsMatch(password) ? PasswordHasherErrors.PasswordTooWeak : BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public bool IsCorrectPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
    }
}