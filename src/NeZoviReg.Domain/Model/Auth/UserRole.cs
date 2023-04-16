namespace NeZoviReg.Domain.Model.Auth;

public class UserRole  : Entity
{
    public UserRole(int userId, int roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

    public int UserId { get; private set; }

    public int RoleId { get; private set; }
}