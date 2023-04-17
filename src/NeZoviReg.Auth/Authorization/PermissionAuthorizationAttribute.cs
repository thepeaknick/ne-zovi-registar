using Microsoft.AspNetCore.Authorization;
using NeZoviReg.Auth.Enum;

namespace NeZoviReg.Auth.Authorization;

public class PermissionAuthorizationAttribute : AuthorizeAttribute
{
    public const string PolicyPrefix = "permission_";

    public PermissionAuthorizationAttribute(PermissionType permission) => Permission = permission.ToString();

    public string? Permission
    {
        get => Policy?.Substring(PolicyPrefix.Length);
        set => Policy = $"{PolicyPrefix}{value}";
    }
}