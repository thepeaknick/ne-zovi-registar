using NeZoviReg.Abstractions.Shared.Model.Auth;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Auth.Authentication.Jwt;

public interface IJwtProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <param name="expirationOffset">expiration offset in minutes</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TokenResult> GenerateTokenAsync(RegUser user, int? expirationOffset = default, CancellationToken cancellationToken = default);
    

    Task<RefreshTokenResult> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken);
}
