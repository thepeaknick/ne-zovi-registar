using NeZoviReg.Domain.Model.Domain;
#pragma warning disable CS8618

namespace NeZoviReg.Domain.Model.Auth;

public class UserAccountRole : Entity
{
    private UserAccountRole(int userId, int roleId)
    {
        RoleId = roleId;
        UserId = userId;
    }
    
    public static UserAccountRole Create(int userId, int roleId)
    {
        var data = new UserAccountRole(userId, roleId);
        data.AddCreation();

        return data;
    }

    public int RoleId { get; private set; }
    public Role Role { get; private set; }

    public int UserId { get; private set; }
    public UserAccount UserAccount { get; private set; }


    
}