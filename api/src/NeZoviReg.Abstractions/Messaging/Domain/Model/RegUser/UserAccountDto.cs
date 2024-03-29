namespace NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;

/// <summary>
/// Korisnički nalog korisnika registra.
/// </summary>
/// <param name="RegUserId">ID korisnika registra</param>
/// <param name="Username">Korisničko ime</param>
/// <param name="Password">Lozinka</param>
public sealed record UserAccountDto(int RegUserId, string Username, string Password);