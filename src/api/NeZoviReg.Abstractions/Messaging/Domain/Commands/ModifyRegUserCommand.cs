using RoleType = NeZoviReg.Abstractions.Shared.Model.Auth.Enum.RoleType;

namespace NeZoviReg.Abstractions.Messaging.Domain.Commands;

public record ModifyRegUserCommand
    (Guid UserId, string Email, string UserName, string Password, string FirstName, string LastName, RoleType[] Rolles)
    : BaseCommand<string>;
