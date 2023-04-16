using NeZoviReg.Domain;

namespace NeZoviReg.Auth.Model;

public class Permission : EnumerationEntity
{
    public Permission(int id, string name)
        :base(id, name)
    {
    }
}