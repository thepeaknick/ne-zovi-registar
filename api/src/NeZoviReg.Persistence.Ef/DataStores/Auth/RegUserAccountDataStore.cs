using Microsoft.EntityFrameworkCore;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Domain.Extensions;
using NeZoviReg.Domain.Model.Auth;

namespace NeZoviReg.Persistence.Ef.DataStores.Auth;

public class RegUserAccountDataStore : IRegUserAccountDataStore
{
    private readonly NeZoviRegDataContext _dbContext;

    public RegUserAccountDataStore(NeZoviRegDataContext dbcontext)
    {
        _dbContext = dbcontext;
    }

    public async Task<List<RegUserAccount>> GetUserAccounts(int regUserId, CancellationToken cancellationToken) =>
        await _dbContext.Set<RegUserAccount>()
            .Where(x => x.RegUserId == regUserId).ToListAsync(cancellationToken);
    
    public async Task<List<RegUserAccount>> GetUserAccounts(Guid regUserGuid, CancellationToken cancellationToken) =>
        await _dbContext.Set<RegUserAccount>()
            .Include(u => u.RegUser)
            .Where(x => x.RegUser.GuidId == regUserGuid).ToListAsync(cancellationToken);

    public async Task AddOrUpdateUserAccounts(List<RegUserAccount> userAccounts,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<RegUserAccount>()
            .AddRangeAsync(userAccounts, cancellationToken);

    public async Task<RegUserAccount?> GetByUsernameAndPassword(string username, string password,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<RegUserAccount>()
            .Include(u => u.RegUser)
            .SingleOrDefaultAsync(x => x.Username == username && x.Password == password.Encode(), cancellationToken);

    public async Task<RegUserAccount?> GetByEmail(string email, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<RegUserAccount>()
            .Include(u => u.RegUser)
            .SingleOrDefaultAsync(x => x.RegUser.Email == email, cancellationToken);


    public async Task<RegUserAccount?> GetByGuidId(Guid guidId, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<RegUserAccount>()
            .Include(x => x.RegUser)
            .SingleOrDefaultAsync(x => x.GuidId == guidId, cancellationToken);

    public async Task<bool> IsUsernameExistsAsync(string username, Guid? excludeId = default,
        CancellationToken cancellationToken = default)
        => await _dbContext.Set<RegUserAccount>()
            .AnyAsync(regUser => regUser.Username == username && (excludeId == default || regUser.GuidId != excludeId),
                cancellationToken);

    public async Task Add(RegUserAccount regUserAccount, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<RegUserAccount>().AddAsync(regUserAccount, cancellationToken);

    public void Update(RegUserAccount regUserAccount) =>
        _dbContext.Set<RegUserAccount>().Update(regUserAccount);

    public void Remove(RegUserAccount regUserAccount) =>
        _dbContext.Set<RegUserAccount>().Remove(regUserAccount);
}