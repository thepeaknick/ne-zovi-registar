namespace NeZoviReg.Abstractions.Shared.Model.Auth.Enum;

[Flags]
public enum PermissionType
{
    RegUsersOnly = 2,

    Write = 4,

    Delete = 8,

    Read = 16,

    All = RegUsersOnly | Read
}