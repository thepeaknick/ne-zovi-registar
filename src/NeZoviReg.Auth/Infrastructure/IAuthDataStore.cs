using NeZoviReg.Domain.Model;

namespace NeZoviReg.Auth.Infrastructure;

public interface IAuthDataStore
{
    Task<List<string>> GetUserPermissionsAsync(int regUserId, CancellationToken cancellationToken);

    Task<RegUser?> GetRegUserByEmailAsync(string email, CancellationToken cancellationToken);
}