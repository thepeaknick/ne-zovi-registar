using NeZoviReg.Domain.Model.Auth;

namespace NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;

public interface IUserAccountDataStore
{
    Task<List<UserAccount>> GetUserAccounts(int regUserId, CancellationToken cancellationToken);
    
    Task AddOrUpdateUserAccounts(List<UserAccount> userAccounts, CancellationToken cancellationToken = default);
}