using NeZoviReg.Abstractions.Shared.Model.Auth;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Auth.Authentication.Jwt;

public interface IJwtProvider
{
    Task<TokenResult> GenerateTokenAsync(RegUser user, CancellationToken cancellationToken);

    Task<RefreshTokenResult> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken);
}
