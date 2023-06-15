namespace NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;

/// <summary>
/// Detalji korisnika registra
/// </summary>
/// <param name="GuidId">Guid ID korisniak registra</param>
/// <param name="FirstName">Ime korisnika registra</param>
/// <param name="LastName">Prezime korisniak registra</param>
/// <param name="CompanyName">Naziv firme</param>
/// <param name="Address">Adresa</param>
/// <param name="RegNumber">Matični broj</param>
/// <param name="TaxNumber">PIB</param>
/// <param name="Role">Rola korisnika registra</param>
public sealed record RegUserDetailsDto(Guid GuidId,
    string FirstName,
    string LastName,
    string CompanyName,
    string Address,
    string RegNumber,
    string TaxNumber,
    int Role);