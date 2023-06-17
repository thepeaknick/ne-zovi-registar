namespace NeZoviReg.WebApi.Model.Token;

/// <summary>
/// Zahtev za osvežavanje tokena.
/// </summary>
/// <param name="AccessToken">Pristupni token</param>
/// <param name="RefreshToken">Token za osvežavanje</param>
public record RefreshTokenRequest(string AccessToken, string RefreshToken);