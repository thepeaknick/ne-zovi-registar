using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;

namespace NeZoviReg.WebApi.Model.RegUser;

/// <summary>
/// Zahtev za izmenu podataka korisnika registra.
/// </summary>
/// <param name="Name">Naziv</param>
/// <param name="Email">Email adresa</param>
/// <param name="Address">Adresa</param>
/// <param name="RegNumber">Matični broj</param>
/// <param name="TaxNumber">Pib</param>
/// <param name="FirstName">Ime korisnika</param>
/// <param name="LastName">Preyime korisnika</param>
/// <param name="UserName">Korisničko ime korisnika</param>
/// <param name="Role">Rola korisnika registra</param>
public record ModifyRegUserRequest(string? Name = default,
    string? Email = default,
    string? Address = default,
    string? RegNumber = default,
    string? TaxNumber = default,
    string? FirstName = default,
    string? LastName = default,
    string? UserName = default,
    RoleType? Role = default);