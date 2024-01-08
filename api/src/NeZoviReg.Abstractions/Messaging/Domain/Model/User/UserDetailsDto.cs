namespace NeZoviReg.Abstractions.Messaging.Domain.Model.User;


/// <summary>
/// Detalji vlasnika registrovanog broj telefona.
/// <param name="PhoneNumber">Broj telefona</param>
/// <param name="Firstname">Ime vlasnika</param>
/// <param name="LastName">Prezime vlasnika</param>
/// <param name="OperatorId">Id obveznika</param>
/// </summary>
public sealed record UserDetailsDto(string PhoneNumber, string Firstname, string LastName, int OperatorId);