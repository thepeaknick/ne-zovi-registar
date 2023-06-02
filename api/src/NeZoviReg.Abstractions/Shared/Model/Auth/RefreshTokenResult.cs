using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Abstractions.Shared.Model.Auth;

public record RefreshTokenResult(RegUser? RegUser, string AccessToken, RefreshToken RefreshToken);