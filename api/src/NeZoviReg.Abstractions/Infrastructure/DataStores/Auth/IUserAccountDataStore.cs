using NeZoviReg.Domain.Model.Auth;

namespace NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;

public interface IUserAccountDataStore
{
    Task<List<RegUserAccount>> GetUserAccounts(int regUserId, CancellationToken cancellationToken);
    
    Task<RegUserAccount?> GetByGuidId(Guid guidId, CancellationToken cancellationToken = default);
    
    Task AddOrUpdateUserAccounts(List<RegUserAccount> userAccounts, CancellationToken cancellationToken = default);
    
    Task<RegUserAccount?> GetByUsernameAndPassword(string username, string password, CancellationToken cancellationToken = default);
    
    Task<RegUserAccount?> GetByEmail(string email, CancellationToken cancellationToken = default);
    
    Task<bool> IsUsernameExistsAsync(string username, Guid? excludeId = default, CancellationToken cancellationToken = default);
    
    Task Add(RegUserAccount regUserAccount, CancellationToken cancellationToken = default);

    void Remove(RegUserAccount regUserAccount);
    
    void Update(RegUserAccount member);
}