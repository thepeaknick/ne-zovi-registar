using NeZoviReg.Abstractions.Messaging.Auth.Model;

namespace NeZoviReg.Abstractions.Messaging.Auth.Commands;

public record LoginCommand(string UserName, string Password) : BaseCommand<LoginResultDto>;
    