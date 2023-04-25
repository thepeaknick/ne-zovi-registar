namespace NeZoviReg.Abstractions.Messaging.Domain.Commands;

public record RemoveRegUserCommand(Guid RegUserId)
    : BaseCommand<string>;
