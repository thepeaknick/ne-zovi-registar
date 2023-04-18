using NeZoviReg.Abstractions.Messaging;

namespace NeZoviReg.Auth.Services.Login;

public record LoginCommand(string Email) : ICommand<string>;
    