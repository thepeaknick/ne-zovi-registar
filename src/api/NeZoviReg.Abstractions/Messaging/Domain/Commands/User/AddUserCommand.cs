namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.User;

public record AddUserCommand
    (string FirstName, string LastName, string Jmbg, string PhoneNumber)
    : BaseCommand<string>;
