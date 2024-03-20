namespace NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;

/// <summary>
/// Detalji korisnika iz APR-a
/// </summary>
/// <param name="FirstName">Ime odgovornog lica</param>
/// <param name="LastName">Prezime odgovornog lica</param>
/// <param name="CompanyName">Naziv firme</param>
/// <param name="Address">Adresa</param>
/// <param name="RegNumber">Matični broj</param>
/// <param name="TaxNumber">PIB</param>
public sealed record RegUserAprDetailsDto(string CompanyName, 
    string Address,
    string RegNumber,
    string TaxNumber,
    string FirstName,
    string LastName);