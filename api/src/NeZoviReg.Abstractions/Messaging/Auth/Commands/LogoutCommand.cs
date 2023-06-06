namespace NeZoviReg.Abstractions.Messaging.Auth.Commands;

public record LogoutCommand(Guid GuidId) : BaseCommand<bool>;
    