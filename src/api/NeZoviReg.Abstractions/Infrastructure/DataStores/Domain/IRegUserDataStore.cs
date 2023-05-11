using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;

public interface IRegUserDataStore
{
    Task<bool> IsRegNumberUniqueAsync(string regNumber, Guid? excludeId = default, CancellationToken cancellationToken = default);

    Task<bool> IsCompanyNameUniqueAsync(string name, Guid? excludeId = default, CancellationToken cancellationToken = default);

    Task<bool> IsTaxNumberUniqueAsync(string taxNumber, Guid? excludeId = default, CancellationToken cancellationToken = default);

    Task<bool> IsUsernamelUniqueAsync(string username, Guid? excludeId = default, CancellationToken cancellationToken = default);

    Task<RegUser?> GetByUsernameAndPassword(string username, string password, CancellationToken cancellationToken = default);

    //Task<RegUser?> GetByThumbprint(string thumbprint, CancellationToken cancellationToken = default);

    Task<RegUser?> GetById(int regUserId, CancellationToken cancellationToken = default);

    Task<RegUser?> GetByGuidId(Guid regUserId, CancellationToken cancellationToken = default);

    Task<List<RegUser>> GetByRole(RoleType role, CancellationToken cancellationToken = default);

    Task Add(RegUser regUser, CancellationToken cancellationToken = default);

    void Remove(RegUser member);

    void Update(RegUser member);
}