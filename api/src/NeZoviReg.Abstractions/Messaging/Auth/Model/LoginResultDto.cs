namespace NeZoviReg.Abstractions.Messaging.Auth.Model;

/// <summary>
/// Prijavljen korisnik registra.
/// </summary>
/// <param name="RegUserId">Guid Id korsnika registra</param>
/// <param name="AccessToken">Pristupni token</param>
/// <param name="AccessTokenExpTime">Datum i vreme isteka pristupnog tokena</param>
/// <param name="RefreshToken">Token za osvežavanje</param>
/// <param name="RefreshTokenExpTime">Datum i vreme isteka tokena za osvežavanje</param>
public record LoginResultDto(Guid RegUserId, string AccessToken, DateTime AccessTokenExpTime, string RefreshToken, DateTime RefreshTokenExpTime);