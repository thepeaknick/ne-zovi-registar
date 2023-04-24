using Microsoft.AspNetCore.Authorization;
using NeZoviReg.Auth.Model.Enum;

namespace NeZoviReg.Auth.Authorization;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public const string PolicyPrefix = "permission_";

    public HasPermissionAttribute(PermissionType permission) => Permission = permission.ToString();

    public string? Permission
    {
        get => Policy?.Substring(PolicyPrefix.Length);
        set => Policy = $"{PolicyPrefix}{value}";
    }
}