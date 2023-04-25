using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Auth.Infrastructure;

public interface IAuthDataStore
{
    Task<List<string>> GetUserPermissionsAsync(Guid regUserId, CancellationToken cancellationToken);

    Task<RegUser?> GetRegUserByEmailAsync(string email, CancellationToken cancellationToken);
}