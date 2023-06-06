using NeZoviReg.Abstractions.Shared.Model.Auth;

namespace NeZoviReg.Abstractions.Messaging.Auth.Model;

public record RefreshTokenResultDto(string AccessToken, RefreshToken RefreshToken);