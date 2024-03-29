using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;

public interface IRegUserDataStore
{
    Task<bool> IsRegNumberExistsAsync(string regNumber, Guid? excludeId = default, CancellationToken cancellationToken = default);

    Task<bool> IsCompanyNameExistsAsync(string name, Guid? excludeId = default, CancellationToken cancellationToken = default);

    Task<bool> IsTaxNumberExistsAsync(string taxNumber, Guid? excludeId = default, CancellationToken cancellationToken = default);

    Task<bool> IsUsernameExistsAsync(string username, Guid? excludeId = default, CancellationToken cancellationToken = default);
    
    Task<bool> IsEmailExistsAsync(string email, Guid? excludeId = default, CancellationToken cancellationToken = default);

    Task<RegUser?> GetByUsernameAndPassword(string username, string password, CancellationToken cancellationToken = default);
    
    Task<RegUser?> GetByEmail(string email, CancellationToken cancellationToken = default);

    Task<RegUser?> GetById(int regUserId, CancellationToken cancellationToken = default);

    Task<RegUser?> GetByGuidId(Guid regUserId, CancellationToken cancellationToken = default);

    Task<RegUser?> GetWithAccountsByGuidId(Guid regUserId, CancellationToken cancellationToken = default);

    Task<List<RegUser>> GetByRole(RoleType role, CancellationToken cancellationToken = default);

    Task Add(RegUser regUser, CancellationToken cancellationToken = default);

    void Remove(RegUser member);

    void Update(RegUser member);
}