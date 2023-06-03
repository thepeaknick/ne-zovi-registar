using NeZoviReg.Abstractions.Shared.Model.Auth;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Domain.Model.Auth;

namespace NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;

public interface IAuthDataStore
{
    Task<RegUserWithPermissions> GetUserWithPermissionsAsync(Guid regUserId, CancellationToken cancellationToken);

    Task<List<Role>> GetRollesAsync(CancellationToken cancellationToken);

    Task<List<Role>> GetRollesAsync(RoleType[] rolles, CancellationToken cancellationToken);
}