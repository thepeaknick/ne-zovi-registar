using NeZoviReg.Abstractions.Messaging.Domain.Model;

namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;

public record RemoveRegUserCommand(Guid RegUserId)
    : BaseCommand<RegUserDto>;
