using NeZoviReg.Domain.Model.Domain;
#pragma warning disable CS8618

namespace NeZoviReg.Domain.Model.Auth;

public class RegUserRole : Entity
{
    public RegUserRole(int regUserId, int roleId)
    {
        RoleId = roleId;
        RegUserId = regUserId;
    }

    public int RoleId { get; private set; }
    public Role Role { get; private set; }

    public int RegUserId { get; private set; }
    public RegUser RegUser { get; private set; }


    public static RegUserRole Create(int regUserId, int roleId)
    {
        return new RegUserRole(regUserId, roleId);
    }
}