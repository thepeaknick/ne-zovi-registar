using RoleType = NeZoviReg.Abstractions.Shared.Model.Auth.Enum.RoleType;

namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;

public record CreateRegUserCommand
    (string Email, string UserName, string Password, string FirstName, string LastName, RoleType[] Roles, string? Thumbprint = null)
    : BaseCommand<string>;
