namespace NeZoviReg.Abstractions.Messaging.Auth.Model;

public record LoginResultDto(string AccessToken, string RefreshToken);