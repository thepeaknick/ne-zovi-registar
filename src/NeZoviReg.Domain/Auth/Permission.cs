namespace NeZoviReg.Domain.Auth;

public class Permission : Entity
{
    public Permission()
    {
    }

    public Permission(int id, string name)
        :base(id)
    {
        Name = name;
    }

    public string Name { get; private set; } = string.Empty;
}