using NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;

namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;

public record ModifyRegUserAccountsCommand(Guid RegUserId, List<UserAccountData> Accounts) : BaseCommand<bool>;
