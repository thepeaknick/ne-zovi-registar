using Microsoft.AspNetCore.Authorization;
using NeZoviReg.Auth.Model.Enum;

namespace NeZoviReg.Auth;

public class RoleRequirement: IAuthorizationRequirement
{
    public RoleRequirement(RoleType role) =>
        Role = role;

    public RoleType Role { get; }
}