namespace NeZoviReg.Abstractions.Messaging.Domain.Model.User;

/// <summary>
/// Registrovan broj telefona.
/// </summary>
/// <param name="PhoneNumber">Broj telefona</param>
/// <param name="CreatedModifiedOn">Datume i vreme registracije/izmene</param>
public sealed record UserDto(string PhoneNumber, DateTime? CreatedModifiedOn);