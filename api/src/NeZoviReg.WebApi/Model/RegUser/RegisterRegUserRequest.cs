using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;

namespace NeZoviReg.WebApi.Model.RegUser;

/// <summary>
/// Zahtev za regostraciju korisnia regisra
/// </summary>
/// <param name="Name">Naziv</param>
/// <param name="Email">Email adresa</param>
/// <param name="Address">Adresa</param>
/// <param name="RegNumber">Matični broj</param>
/// <param name="TaxNumber">Pib</param>
/// <param name="FirstName">Ime korisnika</param>
/// <param name="LastName">Preyime korisnika</param>
/// <param name="UserName">Korisničko ime korisnika</param>
/// <param name="Password">Lozinka</param>
/// <param name="Roles">Role korisnika registra</param>
public record RegisterRegUserRequest(string Name,
    string Email,
    string Address,
    string RegNumber,
    string TaxNumber,
    string FirstName,
    string LastName,
    string UserName,
    string Password,
    RoleType[] Roles);