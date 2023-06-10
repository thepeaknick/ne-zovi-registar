using NeZoviReg.Abstractions.Messaging.Domain.Model;

namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;

public record ChangePassCommand(string UserName, string Password, string NewPassword)
    : BaseCommand<bool>;
