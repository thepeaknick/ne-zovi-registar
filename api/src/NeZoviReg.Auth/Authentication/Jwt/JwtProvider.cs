using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Shared.Model;
using NeZoviReg.Abstractions.Shared.Model.Auth;
using NeZoviReg.Auth.Exceptions;
using RegUser = NeZoviReg.Domain.Model.Domain.RegUser;

namespace NeZoviReg.Auth.Authentication.Jwt;

internal sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    private readonly IRegUserDataStore _regUserDataStore;

    public JwtProvider(IOptions<JwtOptions> options, IRegUserDataStore regUserDataStore)
    {
        _regUserDataStore = regUserDataStore;
        _options = options.Value;
    }

    public Task<TokenResult> GenerateTokenAsync(RegUser user, CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new(CustomClaims.RegUserId, user.GuidId.ToString()),
            new(CustomClaims.RegUserName, user.Username)
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                                     SecurityAlgorithms.HmacSha256Signature);

        /*var permissions = await _authDataStore.GetUserPermissionsAsync(user.Id, cancellationToken);

        foreach (string permission in permissions)
        {
            claims.Add(new(CustomClaims.Permissions, permission));
        }*/

        var now = DateTime.Now;
        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            now.AddMinutes(_options.AccesTokenExpirationInMinutes),
            signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        var refreshToken = new RefreshToken(GenerateRefreshTokenString(),
            now.AddDays(_options.RefreshTokenExpirationInDays));

        return Task.FromResult(new TokenResult(tokenValue, token.ValidTo.ToLocalTime(), refreshToken));
    }

    private static string GenerateRefreshTokenString()
    {
        var randomNumber = new byte[32];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public async Task<RefreshTokenResult> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken)
    {
        var now = DateTime.Now;

        PrincipalWithToken pandt;
        pandt = DecodeJwtToken(accessToken);

        if (pandt.JwtToken is null || !pandt.JwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
        {
            throw new SecurityTokenInvalidSignatureException("Invalid token. Token algorithm is wrong.");
        }

        var appUser = AppUser.GetUser(pandt.Principal)
                      ?? throw new SecurityTokenException("Invalid token. Claims are wrong.");

        var regUser = await _regUserDataStore.GetByGuidId(appUser.Id, cancellationToken)
                      ?? throw new SecurityTokenException($"Invalid token. RegUser with GuidId={appUser.Id} doesn't exist");

        if (regUser.RefreshToken is null)
        {
            throw new RefreshTokenEmptyException("Invalid token, RegUser.RefreshToken is empty.");
        }

        if (regUser.RefreshToken != refreshToken || regUser.RefreshTokenExpirationTime < now)
        {
            throw new RefreshTokenExpiredException($"Invalid token, RegUser.RefreshToken={regUser.RefreshToken}");
        }

        var tokens = await GenerateTokenAsync(regUser, cancellationToken);

        return new RefreshTokenResult(regUser, tokens.AccessToken, tokens.RefreshToken);
    }

    private PrincipalWithToken DecodeJwtToken(string token)
    {
        var principal = new JwtSecurityTokenHandler()
            .ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidIssuer = _options.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                    ValidAudience = _options.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                },
                out var validatedToken);

        return new (principal, validatedToken as JwtSecurityToken);
    }


    private record PrincipalWithToken(ClaimsPrincipal Principal, JwtSecurityToken? JwtToken);
}
