using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Domain.Model.Auth;
using NeZoviReg.Domain.Model.Domain;

#pragma warning disable CS8618

namespace NeZoviReg.Abstractions.Shared.Model.Auth;

public class RegUserWithPermissions
{
    public RegUser RegUser { get; init; }

    public List<PermissionType>? Permissions  { get; init; }
    
    /// <summary>
    /// Returns RegUserAccount
    /// </summary>
    /// <param name="userAccountGuidId"></param>
    /// <returns></returns>
    public RegUserAccount RegUserAccount(Guid userAccountGuidId)
    {
        return RegUser?.UserAccounts.FirstOrDefault(x => x.GuidId == userAccountGuidId) ?? NeZoviReg.Domain.Model.Auth.RegUserAccount.New;
    }
}