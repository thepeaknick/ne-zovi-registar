using NeZoviReg.Domain.Model.Auth;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Abstractions.Shared.Model.Auth;

public record RefreshTokenResult(UserAccount? UserAccount, string AccessToken, DateTime AccessTokenExpTime, RefreshToken RefreshToken);