using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Application.Infrastructure.DataStores;

public interface IRegUserDataStore
{
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);

    void Add(RegUser regUser);

    void Update(RegUser member);
}