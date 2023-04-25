using Microsoft.EntityFrameworkCore;
using NeZoviReg.Auth.Infrastructure;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Persistence.Ef.DataStores.Auth;

public class AuthDataStore : IAuthDataStore
{
    private readonly NeZoviRegDataContext _dbContext;

    public AuthDataStore(NeZoviRegDataContext context)
    {
        _dbContext = context;
    }

    public async Task<List<string>> GetUserPermissionsAsync(Guid regUserId, CancellationToken cancellationToken)
    {
        var roles = await _dbContext.Set<RegUser>()
            .Include(ru => ru.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(ru => ru.GuidId == regUserId)
            .Select(ru => ru.Roles).ToArrayAsync(cancellationToken);

        return roles.SelectMany(r => r)
            .SelectMany(r => r.Permissions)
            .Select(p => p.Name)
            .ToList();

    }

    public async Task<RegUser?> GetRegUserByEmailAsync(string email, CancellationToken cancellationToken)
    => await _dbContext.Set<RegUser>()
            .FirstOrDefaultAsync(ru => ru.Email == email);
}