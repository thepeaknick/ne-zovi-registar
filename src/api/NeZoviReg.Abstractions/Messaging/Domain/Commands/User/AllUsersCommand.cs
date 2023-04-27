namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.User;

public record AllUsersCommand(DateTime StartingFrom) : BaseCommand<string>;
