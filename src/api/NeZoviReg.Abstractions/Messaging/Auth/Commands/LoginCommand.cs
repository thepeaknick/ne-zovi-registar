namespace NeZoviReg.Abstractions.Messaging.Auth.Commands;

public record LoginCommand(string Email) : BaseCommand<string>;
    