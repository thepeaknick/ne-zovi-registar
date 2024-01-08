using NeZoviReg.Abstractions.Extensions.Paging;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;

public interface IUserDataStore
{
    Task<User?> GetByPhoneNumber(string phoneNumber, CancellationToken cancellationToken = default);

    Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber, CancellationToken cancellationToken = default);

    Task<PagedList<User>> GetAll(DateTime? after, PageInfo pInfo, CancellationToken cancellationToken = default);

    Task AddAsync(User user, CancellationToken cancellationToken = default);
    
    Task BulkAddAsync(List<User> users, CancellationToken cancellationToken = default);

    Task BulkRemoveAsync(List<string> phoneNumbers, CancellationToken cancellationToken = default);

    void Update(User user);
}