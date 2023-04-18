using NeZoviReg.Domain.Model;

namespace NeZoviReg.Auth.Authentication.Jwt;

public interface IJwtProvider
{
    Task<string> GenerateAsync(RegUser user, CancellationToken cancellationToken);
}
