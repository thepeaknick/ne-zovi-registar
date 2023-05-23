namespace NeZoviReg.Abstractions.Shared.Model.Auth;

public record RefreshToken(string TokenString, DateTime ExpireAt);