namespace NeZoviReg.WebApi.Model.Token;

public record RefreshTokenRequest(string AccessToken, string RefreshToken);