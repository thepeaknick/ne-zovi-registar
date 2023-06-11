using NeZoviReg.Abstractions.Messaging.Domain.Model.User;

namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.User;

public record AddUserCommand(string FirstName, string LastName, string[] PhoneNumbers, string Jmbg, int OperatorId)
    : BaseCommand<List<UserDto>>;
