namespace Shopify.Infrastructure.Authentication.JwtToken;

internal sealed class JwtSettings
{
    public const string Section = "JwtSettings";

    public string Audience { get; init; } = null!;
    public string Issuer { get; init; } = null!;
    public string Secret { get; init; } = null!;
    public int TokenExpirationInMinutes { get; init; }
}