using Microsoft.AspNetCore.Authorization;
using NeZoviReg.Auth.Model;

namespace NeZoviReg.Auth;

public class PermissionRequirement: IAuthorizationRequirement
{
    public PermissionRequirement(string permission) =>
        Permission = permission;

    public string Permission { get; }
}