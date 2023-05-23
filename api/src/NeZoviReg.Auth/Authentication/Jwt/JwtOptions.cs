namespace NeZoviReg.Auth.Authentication.Jwt;

public class JwtOptions
{
    public string Issuer { get; init; } = string.Empty;

    public string Audience { get; init; } = string.Empty;

    public string SecretKey { get; init; } = string.Empty;

    public int AccesTokenExpirationInMinutes { get; init; } = 15;

    public int RefreshTokenExpirationInDays { get; init; } = 1;
}