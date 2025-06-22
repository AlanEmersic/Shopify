namespace Shopify.Infrastructure.Authentication.JwtToken;

internal sealed class JwtSettings
{
    public const string Section = "JwtSettings";

    public string Audience { get; init; }
    public string Issuer { get; init; }
    public string Secret { get; init; }
    public int TokenExpirationInMinutes { get; init; }
}