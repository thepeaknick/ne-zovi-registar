using NeZoviReg.Auth.Model;
using NeZoviReg.Auth.Model.Enum;
using NeZoviReg.Domain;

namespace NeZoviReg.Auth.Model;

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