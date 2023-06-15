namespace NeZoviReg.WebApi.Model.User;

/// <summary>
/// Zahtev za izmenu padataka vlasnika broja telefona.
/// </summary>
/// <param name="FirstName">Ime vlasnika broja telefona</param>
/// <param name="LastName">Prezime vlsnika broja telefona</param>
/// <param name="Jmbg">JMBG vlasnika broja telefona</param>
/// <param name="PhoneNumber">Broj telefona</param>
/// <param name="OperatorId">Id operatora</param>
public record ModifyUserRequest(string? FirstName = null, string? LastName = null, string? Jmbg = null, string? PhoneNumber = null, int? OperatorId = default);