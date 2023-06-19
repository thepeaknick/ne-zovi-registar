namespace NeZoviReg.Abstractions.Shared.Model.Domain;

/// <summary>
/// Bulk vlasnik broja telefona.
/// </summary>
/// <param name="FirstName">Ime vlasnika broja telefona</param>
/// <param name="LastName">Prezime vlasnika broja telefona</param>
/// <param name="Jmbg">JMBG vlasnika broja telefona</param>
/// <param name="OperatorId">Id operatora(obveznika)</param>
/// <param name="PhoneNumber">Broj telefona</param>
public record BulkUser(string FirstName, string LastName, string Jmbg, int OperatorId, string PhoneNumber);