using NeZoviReg.Abstractions.Shared.Model.Auth;

namespace NeZoviReg.Abstractions.Messaging.Auth.Model;

public record RefreshTokenResultDto(string AccessToken, DateTime AccessTokenExpTime, string RefreshToken, DateTime RefreshTokenExpTime);