namespace NeZoviReg.WebApi.Model.User;

/// <summary>
/// Zahtev za registraciju broj telefona.
/// </summary>
/// <param name="FirstName">Ime vlasnika broja telefona</param>
/// <param name="LastName">Prezime vlsnika broja telefona</param>
/// <param name="Jmbg">JMBG vlasnika broja telefona</param>
/// <param name="OperatorId">Id operatora(obveznika)</param>
/// <param name="PhoneNumbers">Brojevi telefona</param>
public record AddUserRequest(string FirstName, string LastName, string Jmbg, int OperatorId, string[] PhoneNumbers);