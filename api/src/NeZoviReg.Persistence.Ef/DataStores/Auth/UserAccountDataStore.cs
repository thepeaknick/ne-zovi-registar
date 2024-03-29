using Microsoft.EntityFrameworkCore;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
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

    public async Task AddOrUpdateUserAccounts(List<UserAccount> userAccounts, CancellationToken cancellationToken = default)=>
        await _dbContext.Set<UserAccount>().AddRangeAsync(userAccounts, cancellationToken);
}