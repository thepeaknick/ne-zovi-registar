using NeZoviReg.Domain.Model.Domain;
#pragma warning disable CS8618

namespace NeZoviReg.Domain.Model.Auth;

public class RegUserRole : Entity
{
    public static RegUserRole Create(int regUserId, int roleId)
    {
        var rp =  new RegUserRole {RoleId = roleId, RegUserId = regUserId};
        rp.AddCreation();
        
        return rp;
    }
    
    public static RegUserRole New(int regUserId, int roleId)
    {
        var rp =  new RegUserRole {RoleId = roleId, RegUserId = regUserId};
       
        return rp;
    }

    public int RoleId { get; private set; }
    public Role Role { get; private set; }

    public int RegUserId { get; private set; }
    public RegUser RegUser { get; private set; }
    
}