using NeZoviReg.Abstractions.Messaging.Domain.Model;

namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;

public record ChangePassRegUserCommand
    (string UserName, string Password, string NewPassword)
    : BaseCommand<RegUserDto>;
