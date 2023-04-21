using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NeZoviReg.Auth.Authentication.Services;
using NeZoviReg.Auth.Infrastructure;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Auth.Authentication.Jwt;

internal sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    private readonly IAuthDataStore _authDataStore;

    public JwtProvider(IOptions<JwtOptions> options, IAuthDataStore authDataStore)
    {
        _authDataStore = authDataStore;
        _options = options.Value;
    }

    public Task<string> GenerateAsync(RegUser user, CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new(CustomClaims.RegUserId, user.Id.ToString()),
            new(CustomClaims.RegUserName, user.Username)
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                                     SecurityAlgorithms.HmacSha256);

        /*var permissions = await _authDataStore.GetUserPermissionsAsync(user.Id, cancellationToken);

        foreach (string permission in permissions)
        {
            claims.Add(new(CustomClaims.Permissions, permission));
        }*/

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(1),
            signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return Task.FromResult(tokenValue);
    }
}
