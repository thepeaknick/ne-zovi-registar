using NeZoviReg.Abstractions.Messaging.Domain.Model.UserAccount;

namespace NeZoviReg.Abstractions.Messaging.Domain.Queries.RegUser;

public record RegUserAccountsQuery(Guid RegUserId, string CurrentUsername) : IQuery<List<UserAccountData>>;
