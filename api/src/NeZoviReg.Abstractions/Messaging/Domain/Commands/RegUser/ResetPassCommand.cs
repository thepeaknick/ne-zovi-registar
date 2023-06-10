namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;

public record ResetPassCommand(string Email, string Token, string Password)
    : BaseCommand<bool>;
