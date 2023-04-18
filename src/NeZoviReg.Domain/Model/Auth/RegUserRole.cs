namespace NeZoviReg.Domain.Model.Auth;

public class RegUserRole : Entity
{
    public RegUserRole(int regUserId, int roleId)
    {
        RoleId = roleId;
        RegUserId = regUserId;
    }

    public int RoleId { get; private set; }

    public int RegUserId { get; private set; }
}