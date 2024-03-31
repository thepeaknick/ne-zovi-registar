using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Domain.Model.Auth;

#pragma warning disable CS8618

namespace NeZoviReg.Abstractions.Shared.Model.Auth;

public class UserAccountWithPermissions
{
    public UserAccount UserAccount { get; init; }

    public List<PermissionType>? Permissions  { get; init; }
}