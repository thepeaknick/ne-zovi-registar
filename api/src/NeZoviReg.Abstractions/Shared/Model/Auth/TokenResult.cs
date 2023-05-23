using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Abstractions.Shared.Model.Auth;

public record TokenResult(string AccessToken, RefreshToken RefreshToken);