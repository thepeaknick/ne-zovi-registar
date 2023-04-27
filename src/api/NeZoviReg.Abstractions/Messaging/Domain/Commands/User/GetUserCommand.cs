namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.User;

public record GetUserCommand(string PhoneNumber) : BaseCommand<string>;
