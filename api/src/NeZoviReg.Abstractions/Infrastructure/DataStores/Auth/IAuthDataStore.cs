using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Domain.Model.Auth;

namespace NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;

public interface IAuthDataStore
{
    Task<List<string>> GetUserPermissionsAsync(Guid regUserId, CancellationToken cancellationToken);

    Task<List<Role>> GetRollesAsync(CancellationToken cancellationToken);

    Task<List<Role>> GetRollesAsync(RoleType[] rolles, CancellationToken cancellationToken);
}