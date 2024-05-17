using NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;

namespace NeZoviReg.Abstractions.Messaging.Domain.Queries.RegUser;

public record RegUserAccountsQuery(Guid RegUserId, string CurrentUsername) : IQuery<RegUserAccountsDto>;
