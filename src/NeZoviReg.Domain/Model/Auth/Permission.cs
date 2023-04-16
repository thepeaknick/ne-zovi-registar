namespace NeZoviReg.Domain.Model.Auth;

public class Permission  : Entity
{
    public Permission(string name, string code)
    {
        Name = name;
        Code = code;
    }

    public string Code { get; private set; }

    public string Name { get; private set; }
}