using Microsoft.AspNetCore.Authorization;
using NeZoviReg.Auth.Model.Enum;

namespace NeZoviReg.Auth;

public class RoleAuthorizationAttribute : AuthorizeAttribute
{
    public const string PolicyPrefix = "role_";

    public RoleAuthorizationAttribute(RoleType role) =>
        Role = Enum.Format(typeof(RoleType), role, "g");

    public string? Role
    {
        get => Policy?.Substring(PolicyPrefix.Length);
        set => Policy = $"{PolicyPrefix}{value}";
    }
}