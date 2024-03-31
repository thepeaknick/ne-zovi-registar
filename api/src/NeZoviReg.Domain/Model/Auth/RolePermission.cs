using System.Dynamic;

namespace NeZoviReg.Domain.Model.Auth;

public class RolePermission : Entity
{
    public static RolePermission Create(int roleId, int permissionId)
    {
        var rp =  new RolePermission {RoleId = roleId, PermissionId = permissionId};
        rp.AddCreation();
        return rp;
    }

    public int RoleId { get; private set; }

    public int PermissionId { get; private set; }
}