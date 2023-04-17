using NeZoviReg.Domain.Model;

namespace NeZoviReg.Domain.Auth;

public class Role : Entity
{
    public Role()
    {
    }

    public Role(int id, string name):
        base(id)
    {
        Name = name;
    }

    public string Name { get; private set; } = string.Empty;

    private readonly List<RegUser> _users = new();
    public IReadOnlyCollection<RegUser> Users => _users;

    private readonly List<Permission> _permissions = new();
    public IReadOnlyCollection<Permission> Permissions => _permissions;
}