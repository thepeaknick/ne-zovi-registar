namespace NeZoviReg.Abstractions.Messaging.Auth.Model;

public record RefreshTokenResultDto(string AccessToken, string RefreshToken);