namespace NeZoviReg.Abstractions.Messaging.Domain.Model;

public sealed record RegUserDetailsDto(Guid GuidId,
    string FirstName,
    string LastName,
    string CompanyName,
    string Address,
    string RegNumber,
    string TaxNumber,
    int Role);