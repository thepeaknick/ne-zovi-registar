using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;

public interface IRegUserDataStore
{
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);

    Task<bool> IsUsernamelUniqueAsync(string username, CancellationToken cancellationToken = default);

    Task<RegUser?> GetByEmail(string email, CancellationToken cancellationToken = default);

    Task<RegUser?> GetByUsername(string username, CancellationToken cancellationToken = default);

    Task<RegUser?> GetByGuidId(Guid regUserId, CancellationToken cancellationToken = default);

    Task Add(RegUser regUser, CancellationToken cancellationToken = default);

    void Remove(RegUser member);

    void Update(RegUser member);
}