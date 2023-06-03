using Microsoft.IdentityModel.Tokens;

namespace NeZoviReg.Auth.Exceptions;

public class RefreshTokenEmptyException: SecurityTokenException
{
    public RefreshTokenEmptyException(string message)
        : base(message)
    {
    }

    public RefreshTokenEmptyException(string message, SecurityTokenException innerException)
        : base(message, innerException)
    {
    }
}