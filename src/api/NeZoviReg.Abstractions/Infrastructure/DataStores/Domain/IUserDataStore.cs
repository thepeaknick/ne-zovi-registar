using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;

public interface IUserDataStore
{
    Task<User?> GetByPhoneNumber(string phoneNumber, CancellationToken cancellationToken = default);

    Task<List<User>> GetAll(DateTime after, CancellationToken cancellationToken = default);

    void Add(User regUser, CancellationToken cancellationToken = default);

    void Update(User member);
}