namespace NeZoviReg.Abstractions.Messaging.Domain.Commands;

public record AddUserCommand
    (string FirstName, string LastName, string Jmbg, string PhoneNumber)
    : BaseCommand<string>;
    