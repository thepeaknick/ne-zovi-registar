namespace NeZoviReg.Abstractions.Messaging.Domain.Model.UserAccount;

/// <summary>
/// Korisnički nalog korisnika registra.
/// </summary>
/// <param name="Username">Korisničko ime</param>
/// <param name="Password">Lozinka</param>
public sealed record UserAccountData(string Username, string Password);