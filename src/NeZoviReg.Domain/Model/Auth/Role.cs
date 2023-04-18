using NeZoviReg.Domain.Model.Auth.Enum;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Domain.Model.Auth;

public sealed class Role : EnumerationEntity<Role>
{
    public static readonly Role Admin = new ((int)RoleType.Admin, RoleType.Admin.ToString());
    public static readonly Role Obveznik = new ((int)RoleType.Obveznik, RoleType.Obveznik.ToString());
    public static readonly Role Trgovac = new ((int)RoleType.Trgovac, RoleType.Trgovac.ToString());
    public static readonly Role Potrosac = new ((int)RoleType.Potrosac, RoleType.Potrosac.ToString());

    public Role()
    {
    }

    public Role(int id, string name):
        base(id, name)
    {
    }

    private readonly List<RegUser> _users = new();
    public IReadOnlyCollection<RegUser> Users => _users;

    private readonly List<Permission> _permissions = new();
    public IReadOnlyCollection<Permission> Permissions => _permissions;
}