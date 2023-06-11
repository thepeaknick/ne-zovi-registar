using NeZoviReg.Abstractions.Messaging.Domain.Model.User;

namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.User;

public record RemoveUserCommand(string PhoneNumber)
    : BaseCommand<UserDto>;
