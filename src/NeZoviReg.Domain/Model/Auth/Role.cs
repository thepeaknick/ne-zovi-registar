using NeZoviReg.Domain.Model.Auth.Enum;

namespace NeZoviReg.Domain.Model.Auth;

public class Role : Entity
{
    public Role(string name, RoleType type)
    {
        Name = name;
        Type = type;
    }

    public RoleType Type { get; private set; }

    public string Name { get; private set; }
}