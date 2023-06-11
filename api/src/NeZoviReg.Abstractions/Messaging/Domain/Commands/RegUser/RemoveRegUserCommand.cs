namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;

public record RemoveRegUserCommand(Guid RegUserId)
    : BaseCommand<bool>;
