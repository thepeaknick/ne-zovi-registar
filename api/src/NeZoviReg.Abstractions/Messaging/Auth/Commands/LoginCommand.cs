namespace NeZoviReg.Abstractions.Messaging.Auth.Commands;

public record LoginCommand(string UserName, string Password) : BaseCommand<string>;
    