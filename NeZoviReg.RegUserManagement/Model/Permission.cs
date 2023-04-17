using NeZoviReg.Domain;

namespace NeZoviReg.Auth.Model;

public class Permission : EnumerationEntity
{
    public Permission(int id, int code, string name)
        :base(id, code, name)
    {
    }
}