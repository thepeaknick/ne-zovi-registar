using Microsoft.EntityFrameworkCore;
using NeZoviReg.Auth.Infrastructure;
using NeZoviReg.Domain.Model;

namespace NeZoviReg.Persistence.Ef.Auth;

public class AuthDataStore: IAuthDataStore
{
    private readonly SqlLiteDbContext _context;

    public AuthDataStore(SqlLiteDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> GetUserPermissionsAsync(int regUserId, CancellationToken cancellationToken)
    {
        var roles = await _context.Set<RegUser>()
            .Include(ru => ru.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(ru => ru.Id == regUserId)
            .Select(ru => ru.Roles).ToArrayAsync(cancellationToken);

        return roles.SelectMany(r => r)
            .SelectMany(r => r.Permissions)
            .Select(p => p.Name)
            .ToList();

    }
}