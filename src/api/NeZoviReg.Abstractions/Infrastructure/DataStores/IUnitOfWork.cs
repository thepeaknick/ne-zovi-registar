namespace NeZoviReg.Abstractions.Infrastructure.DataStores;

public interface IUnitOfWork
{
    Task SaveChangesAsync(string user, CancellationToken cancellationToken = default);
}