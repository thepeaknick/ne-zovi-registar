namespace NeZoviReg.Abstractions.Messaging.Domain.Model;

public sealed record RegUserDto(Guid GuidId, string Name, int? Id =default);