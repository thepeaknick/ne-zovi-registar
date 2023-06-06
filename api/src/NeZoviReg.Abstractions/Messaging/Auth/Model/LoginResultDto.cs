using NeZoviReg.Abstractions.Shared.Model.Auth;

namespace NeZoviReg.Abstractions.Messaging.Auth.Model;

public record LoginResultDto(string AccessToken, RefreshToken RefreshToken);