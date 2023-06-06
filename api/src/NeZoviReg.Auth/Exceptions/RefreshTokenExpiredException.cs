using Microsoft.IdentityModel.Tokens;

namespace NeZoviReg.Auth.Exceptions;

public class RefreshTokenExpiredException: SecurityTokenException
{
    public RefreshTokenExpiredException(string message)
        : base(message)
    {
    }

    public RefreshTokenExpiredException(string message, SecurityTokenException innerException)
        : base(message, innerException)
    {
    }
}