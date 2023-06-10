namespace NeZoviReg.Abstractions.Shared.Model.Auth;

public record TokenResult(string AccessToken, DateTime AccessTokenExpTime, RefreshToken RefreshToken);