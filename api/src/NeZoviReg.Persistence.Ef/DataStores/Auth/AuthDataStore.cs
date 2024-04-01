using Microsoft.EntityFrameworkCore;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Abstractions.Shared.Model.Auth;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Domain.Model.Auth;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Persistence.Ef.DataStores.Auth;

public class AuthDataStore : IAuthDataStore
{
    private readonly NeZoviRegDataContext _dbContext;

    public AuthDataStore(NeZoviRegDataContext context)
    {
        _dbContext = context;
    }

    public async Task<UserAccountWithPermissions> GetUserWithPermissionsAsync(Guid userAccountGuidId,
        CancellationToken cancellationToken)
    {
        var regUser = await _dbContext.Set<RegUser>()
            .Include(ru => ru.UserAccounts)
            .Include(ru => ru.RegUserRoles)
            .ThenInclude(rur => rur.Role)
            .ThenInclude(r => r.Permissions)
            //.AsSplitQuery()
            .Where(x => x.GuidId == userAccountGuidId)
            .SingleAsync(cancellationToken);

        return new UserAccountWithPermissions
        {
            RegUser = regUser,
            Permissions = regUser.RegUserRoles.Select(r => r)
                .SelectMany(r => r.Role.Permissions)
                .Select(p => (PermissionType) p.Id)
                .ToList()
        };
    }

    public async Task<List<Role>> GetRolesAsync(CancellationToken cancellationToken) =>
        await _dbContext.Set<Role>()
            .ToListAsync(cancellationToken);

    public async Task<List<Role>> GetRolesAsync(RoleType[] roles, CancellationToken cancellationToken) =>
        await _dbContext.Set<Role>()
            .Where(r => roles.Contains((RoleType) r.Id))
            .ToListAsync(cancellationToken);


    public async Task<Role?> GetRoleAsync(RoleType role, CancellationToken cancellationToken) =>
        await _dbContext.Set<Role>()
            .Where(r => role == (RoleType) r.Id)
            .FirstOrDefaultAsync(cancellationToken);
}