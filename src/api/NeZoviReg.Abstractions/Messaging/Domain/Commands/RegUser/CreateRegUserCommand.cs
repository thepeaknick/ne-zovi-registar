using NeZoviReg.Abstractions.Messaging.Domain.Model;
using RoleType = NeZoviReg.Abstractions.Shared.Model.Auth.Enum.RoleType;

namespace NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;

public record CreateRegUserCommand
    (string CompanyName, string Address, string RegNumber, string TaxNumber,  string FirstName,
        string LastName, string UserName, string Password, RoleType[] Roles)
    : BaseCommand<RegUserDto>;
