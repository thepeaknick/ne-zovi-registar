using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Domain.Model.Auth;
using NeZoviReg.Domain.Model.Domain;

#pragma warning disable CS8618

namespace NeZoviReg.Abstractions.Shared.Model.Auth;

public class UserAccountWithPermissions
{
    public RegUser RegUser { get; init; }

    public List<PermissionType>? Permissions  { get; init; }
    
    public UserAccount UserAccount(Guid userAccountId)
    {
        var userAccount =  RegUser?.UserAccounts.FirstOrDefault(x => x.GuidId == userAccountId) ?? NeZoviReg.Domain.Model.Auth.UserAccount.New;

        return userAccount;
    }
}