namespace NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;

/// <summary>
/// Korisnik registra
/// </summary>
/// <param name="GuidId">Guid ID korisniak registra</param>
/// <param name="Name">Naziv</param>
/// <param name="RegNumber">Matičcni broj</param>
/// <param name="TaxNumber">PIB</param>
/// <param name="CreatedOn">Datum i vreme kreiranja</param>
/// <param name="Id">Identifikator</param>
public sealed record RegUserDto(Guid GuidId, string Name, string RegNumber, string TaxNumber, DateTime CreatedOn, int? Id = default);