using Microsoft.EntityFrameworkCore;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Persistence.Ef.DataStores.Domain;

public class RegUserDataStore : IRegUserDataStore
{
    private readonly NeZoviRegDataContext _dbContext;

    public RegUserDataStore(NeZoviRegDataContext dbcontext)
    {
        _dbContext = dbcontext;
    }

    public async Task<bool> IsRegNumberUniqueAsync(string regNumber, Guid? excludeId = default, CancellationToken cancellationToken = default)
        => !await _dbContext
            .Set<RegUser>()
            .AnyAsync(user => user.RegNumber == regNumber && user.GuidId == (excludeId ?? user.GuidId), cancellationToken);

    public async Task<bool> IsTaxNumberUniqueAsync(string taxNumber, Guid? excludeId = default, CancellationToken cancellationToken = default)
        => !await _dbContext
            .Set<RegUser>()
            .AnyAsync(user => user.TaxNumber == taxNumber && user.GuidId == (excludeId ?? user.GuidId), cancellationToken);

    public async Task<bool> IsCompanyNameUniqueAsync(string name, Guid? excludeId = default, CancellationToken cancellationToken = default)
        => !await _dbContext
            .Set<RegUser>()
            .AnyAsync(user => user.CompanyName == name && user.GuidId == (excludeId ?? user.GuidId), cancellationToken);

    public async Task<bool> IsUsernamelUniqueAsync(string username, Guid? excludeId = default, CancellationToken cancellationToken = default)
        => !await _dbContext
            .Set<RegUser>()
            .AnyAsync(user => user.Username == username && user.GuidId == (excludeId ?? user.GuidId), cancellationToken);

    public async Task<RegUser?> GetByGuidId(Guid regUserId, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<RegUser>()
            .Include(u => u.RegUserRoles)
            .SingleOrDefaultAsync(x => x.GuidId == regUserId, cancellationToken);

    public async Task<RegUser?> GetByUsernameAndPassword(string username, string password,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<RegUser>()
            .SingleOrDefaultAsync(x => x.Username == username && x.Password == RegUser.Encode(password), cancellationToken);

    public async Task<List<RegUser>> GetByRole(RoleType role, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<RegUser>()
            .Include(u => u.RegUserRoles)
            .Where(x => x.RegUserRoles.Select(r => r.RoleId).Contains((int)role))
            .ToListAsync(cancellationToken);

    /*public async Task<RegUser?> GetByThumbprint(string thumbprint, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<RegUser>()
            .SingleOrDefaultAsync(x => x.ThumbPrint == thumbprint, cancellationToken);*/

    public async Task Add(RegUser regUser, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<RegUser>().AddAsync(regUser, cancellationToken);

    public void Update(RegUser regUser) =>
        _dbContext.Set<RegUser>().Update(regUser);

    public void Remove(RegUser regUser) =>
        _dbContext.Set<RegUser>().Remove(regUser);
}