namespace NeZoviReg.Abstractions.Messaging.Auth.Model;

public record LoginResultDto(Guid RegUserId, string AccessToken, string RefreshToken);