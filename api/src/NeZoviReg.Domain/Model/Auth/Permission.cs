using NeZoviReg.Domain.Model.Auth.Enum;

namespace NeZoviReg.Domain.Model.Auth;

public sealed class Permission : EnumerationEntity<Permission>
{
    public static readonly Permission RegUsersOnly = new ((int)PermissionType.RegUsersOnly, PermissionType.RegUsersOnly.ToString());
    public static readonly Permission Read = new ((int)PermissionType.Read, PermissionType.Read.ToString());
    public static readonly Permission Delete = new ((int)PermissionType.Delete, PermissionType.Delete.ToString());
    public static readonly Permission Write = new ((int)PermissionType.Write, PermissionType.Write.ToString());


    public Permission(int id, string name)
        :base(id, name)
    {
    }
}