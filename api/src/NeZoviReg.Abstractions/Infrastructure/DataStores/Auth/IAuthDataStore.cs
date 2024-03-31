using NeZoviReg.Abstractions.Shared.Model.Auth;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Domain.Model.Auth;

namespace NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;

public interface IAuthDataStore
{
    Task<UserAccountWithPermissions> GetUserWithPermissionsAsync(Guid userAccountGuidId, CancellationToken cancellationToken);

    Task<List<Role>> GetRolesAsync(CancellationToken cancellationToken);

    Task<List<Role>> GetRolesAsync(RoleType[] roles, CancellationToken cancellationToken);
    
    Task<Role?> GetRoleAsync(RoleType role, CancellationToken cancellationToken);
}