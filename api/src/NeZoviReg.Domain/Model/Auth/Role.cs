using NeZoviReg.Domain.Model.Auth.Enum;

namespace NeZoviReg.Domain.Model.Auth;

public sealed class Role : EnumerationEntity<Role>
{
    public static readonly Role Admin = new ((int)RoleType.Admin, RoleType.Admin.ToString());
    public static readonly Role Obveznik = new ((int)RoleType.Obveznik, RoleType.Obveznik.ToString());
    public static readonly Role Trgovac = new ((int)RoleType.Trgovac, RoleType.Trgovac.ToString());

    public Role(int id, string name):
        base(id, name)
    {
    }

    private readonly List<RegUserRole> _regUserRoles = new();
    public IReadOnlyCollection<RegUserRole> RegUserRoles => _regUserRoles;

    private readonly List<Permission> _permissions = new();
    public IReadOnlyCollection<Permission> Permissions => _permissions;
}