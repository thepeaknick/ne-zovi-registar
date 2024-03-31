using NeZoviReg.Domain.Model.Auth;

namespace NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;

public interface IUserAccountDataStore
{
    Task<List<UserAccount>> GetUserAccounts(int regUserId, CancellationToken cancellationToken);
    
    Task<UserAccount?> GetByGuidId(Guid guidId, CancellationToken cancellationToken = default);
    
    Task AddOrUpdateUserAccounts(List<UserAccount> userAccounts, CancellationToken cancellationToken = default);
    
    Task<UserAccount?> GetByUsernameAndPassword(string username, string password, CancellationToken cancellationToken = default);
    
    Task<UserAccount?> GetByEmail(string email, CancellationToken cancellationToken = default);
    
    Task Add(UserAccount userAccount, CancellationToken cancellationToken = default);

    void Remove(UserAccount userAccount);
    
    void Update(UserAccount member);
}