namespace NeZoviReg.Domain.Model.Auth;

public class RolePermission : Entity
{
    public RolePermission(int roleId, int permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }

    public int RoleId { get; private set; }

    public int PermissionId { get; private set; }
}