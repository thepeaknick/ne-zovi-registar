using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;

public interface IUserDataStore
{
    Task<User?> GetByPhoneNumber(string phoneNumber, CancellationToken cancellationToken = default);

    Task<bool> IsPhoneNumberUniqueAsync(string phoneNumber, CancellationToken cancellationToken = default);

    Task<List<User>> GetAll(DateTime? after, CancellationToken cancellationToken = default);

    Task AddAsync(User user, CancellationToken cancellationToken = default);
    
    Task BulkAddAsync(List<User> users, CancellationToken cancellationToken = default);

    void Update(User user);

    void Remove(User user);
}