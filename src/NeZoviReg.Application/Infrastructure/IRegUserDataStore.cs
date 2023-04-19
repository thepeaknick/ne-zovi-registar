namespace NeZoviReg.Application.Infrastructure;

public interface IRegUserDataStore
{
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);
}