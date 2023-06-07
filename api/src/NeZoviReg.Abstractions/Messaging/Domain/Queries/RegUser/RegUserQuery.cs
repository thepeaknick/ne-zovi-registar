using NeZoviReg.Abstractions.Messaging.Domain.Model;

namespace NeZoviReg.Abstractions.Messaging.Domain.Queries.RegUser;

public record RegUserQuery(Guid RegUserId) : IQuery<RegUserDetailsDto>;
