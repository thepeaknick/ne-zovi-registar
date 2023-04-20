using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NeZoviReg.Application.Infrastructure.DataStores;
using NeZoviReg.Domain;

namespace NeZoviReg.Persistence.Ef;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly NeZoviRegDataContext _dbContext;

    public UnitOfWork(NeZoviRegDataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChangesAsync(string user, CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities(user);

        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities(string user)
    {
        IEnumerable<EntityEntry<IAuditableEntity>> entries =
            _dbContext
                .ChangeTracker
                .Entries<IAuditableEntity>();

        foreach (EntityEntry<IAuditableEntity> entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Entity.AddCreation(user);
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Entity.AddModification(user);
            }
        }
    }
}
