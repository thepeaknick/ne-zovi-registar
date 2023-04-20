using NeZoviReg.Abstractions.Messaging;

namespace NeZoviReg.Application.Services.RegUser;

public record CreateRegUserCommand
    (string Email, string UserName, string Password, string FirstName, string LastName)
    : BaseCommand<string>;
    