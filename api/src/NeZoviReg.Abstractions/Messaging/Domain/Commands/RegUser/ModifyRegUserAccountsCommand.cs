using NeZoviReg.Abstractions.Messaging.Domain.Model.UserAccount;

namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;

public record ModifyRegUserAccountsCommand(Guid RegUserId, List<UserAccountData> Accounts) : BaseCommand<bool>;
