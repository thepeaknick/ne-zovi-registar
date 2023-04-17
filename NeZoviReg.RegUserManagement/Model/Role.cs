using NeZoviReg.Domain;
using NeZoviReg.Domain.Model;

namespace NeZoviReg.Auth.Model;

public class Role : EnumerationEntity
{
    public Role(int id, int code, string name):
        base(id, code, name)
    {
    }

    private readonly List<RegUser> _users = new();
    public IReadOnlyCollection<RegUser> Users => _users;

    private readonly List<Permission> _permissions = new();
    public IReadOnlyCollection<Permission> Permissions => _permissions;
}