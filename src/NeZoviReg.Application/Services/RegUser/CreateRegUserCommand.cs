using NeZoviReg.Abstractions.Messaging;

namespace NeZoviReg.Application.Services.RegUser;

public record CreateRegUserCommand(string Email, string UserName, string Password) : ICommand<string>;
    