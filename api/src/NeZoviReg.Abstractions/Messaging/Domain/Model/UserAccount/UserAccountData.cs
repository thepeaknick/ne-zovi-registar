namespace NeZoviReg.Abstractions.Messaging.Domain.Model.UserAccount;

/// <summary>
/// Korisnički nalog korisnika registra.
/// </summary>
/// <param name="Username">Korisničko ime</param>
/// <param name="Password">Lozinka korisnika</param>
/// <param name="FirstName">Ime korisnika</param>
/// <param name="LastName">Prezime korisnika</param>
public sealed record UserAccountData(string Username, string Password, string? FirstName, string? LastName);