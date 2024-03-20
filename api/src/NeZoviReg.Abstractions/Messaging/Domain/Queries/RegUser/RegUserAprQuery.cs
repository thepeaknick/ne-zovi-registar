using NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;

namespace NeZoviReg.Abstractions.Messaging.Domain.Queries.RegUser;

public record RegUserAprQuery(string RegNumber) : IQuery<RegUserAprDetailsDto>;
