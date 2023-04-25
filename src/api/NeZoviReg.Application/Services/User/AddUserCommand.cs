using NeZoviReg.Abstractions.Messaging;

namespace NeZoviReg.Application.Services.User;

public record AddUserCommand
    (string FirstName, string LastName, string Jmbg, string PhoneNumber)
    : BaseCommand<string>;
    