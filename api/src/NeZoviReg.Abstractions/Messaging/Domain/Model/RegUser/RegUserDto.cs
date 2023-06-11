namespace NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;

public sealed record RegUserDto(Guid GuidId, string Name, string RegNumber, string TaxNumber, DateTime CreatedOn, int? Id = default);