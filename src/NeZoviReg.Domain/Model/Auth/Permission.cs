using NeZoviReg.Domain.Model.Auth.Enum;

namespace NeZoviReg.Domain.Model.Auth;

public sealed class Permission : EnumerationEntity<Permission>
{
    public static readonly Permission All = new ((int)PermissionType.All, PermissionType.All.ToString());
    public static readonly Permission Read = new ((int)PermissionType.Read, PermissionType.Read.ToString());
    public static readonly Permission Delete = new ((int)PermissionType.Delete, PermissionType.Delete.ToString());
    public static readonly Permission ReadAll = new ((int)PermissionType.ReadAll, PermissionType.ReadAll.ToString());
    public static readonly Permission Write = new ((int)PermissionType.Write, PermissionType.Write.ToString());


    public Permission()
    {
    }

    public Permission(int id, string name)
        :base(id, name)
    {
    }
}