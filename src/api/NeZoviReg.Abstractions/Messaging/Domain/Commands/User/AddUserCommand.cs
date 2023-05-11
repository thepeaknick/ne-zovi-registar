namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.User;

public record AddUserCommand
    (string FirstName, string LastName, string PhoneNumber, string Jmbg, int OperatorId)
    : BaseCommand<string>;
