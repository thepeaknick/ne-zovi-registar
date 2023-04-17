namespace NeZoviReg.Auth.Infrastructure;

public interface IAuthDataStore
{
    Task<List<string>> GetUserPermissionsAsync(int regUserId, CancellationToken cancellationToken);
}