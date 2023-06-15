namespace NeZoviReg.Abstractions.Messaging.Auth.Model;

/// <summary>
/// Pristupni token i token za osvežavanje.
/// </summary>
/// <param name="AccessToken">Pristupni token</param>
/// <param name="AccessTokenExpTime">Datum isteka pristupnog tokena</param>
/// <param name="RefreshToken">Token za osvežavanje</param>
/// <param name="RefreshTokenExpTime">Datum isteka tokena za osvežavanje</param>
public record RefreshTokenResultDto(string AccessToken, DateTime AccessTokenExpTime, string RefreshToken, DateTime RefreshTokenExpTime);