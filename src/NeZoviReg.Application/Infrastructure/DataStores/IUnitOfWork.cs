namespace NeZoviReg.Application.Infrastructure.DataStores;

public interface IUnitOfWork
{
    Task SaveChangesAsync(string user, CancellationToken cancellationToken = default);
}