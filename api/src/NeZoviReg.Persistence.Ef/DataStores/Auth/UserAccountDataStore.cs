using Microsoft.EntityFrameworkCore;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Domain.Extensions;
using NeZoviReg.Domain.Model.Auth;

namespace NeZoviReg.Persistence.Ef.DataStores.Auth;

public class UserAccountDataStore : IUserAccountDataStore
{
    private readonly NeZoviRegDataContext _dbContext;

    public UserAccountDataStore(NeZoviRegDataContext dbcontext)
    {
        _dbContext = dbcontext;
    }

    public async Task<List<UserAccount>> GetUserAccounts(int regUserId, CancellationToken cancellationToken) =>
        await _dbContext.Set<UserAccount>().Where(x => x.RegUserId == regUserId).ToListAsync(cancellationToken);

    public async Task AddOrUpdateUserAccounts(List<UserAccount> userAccounts,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<UserAccount>().AddRangeAsync(userAccounts, cancellationToken);

    public async Task<UserAccount?> GetByUsernameAndPassword(string username, string password,
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<UserAccount>()
            .Include(u => u.RegUser)
            .SingleOrDefaultAsync(x => x.Username == username && x.Password == password.Encode(), cancellationToken);

    public async Task<UserAccount?> GetByEmail(string email, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<UserAccount>()
            .Include(u => u.RegUser)
            .SingleOrDefaultAsync(x => x.RegUser.Email == email, cancellationToken);


    public async Task<UserAccount?> GetByGuidId(Guid guidId, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<UserAccount>()
            .SingleOrDefaultAsync(x => x.GuidId == guidId, cancellationToken);

    public async Task Add(UserAccount userAccount, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<UserAccount>().AddAsync(userAccount, cancellationToken);

    public void Update(UserAccount userAccount) =>
        _dbContext.Set<UserAccount>().Update(userAccount);

    public void Remove(UserAccount userAccount) =>
        _dbContext.Set<UserAccount>().Remove(userAccount);
}