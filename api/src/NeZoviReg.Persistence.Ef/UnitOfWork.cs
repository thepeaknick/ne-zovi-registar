using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
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
        UpdateEntitiesState();

        UpdateAuditableEntities(user);

        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities(string user)
    {
        IEnumerable<EntityEntry<IAuditableEntity>> entries =
            _dbContext
                .ChangeTracker
                .Entries<IAuditableEntity>()
                .ToList();

        foreach (EntityEntry<IAuditableEntity> entityEntry in entries)
        {
            if (string.IsNullOrEmpty(user))
            {
                //TEMP for test only
                user = "init";
                //throw new InvalidOperationException($"User is mandatory for saving, appUser={user}.");
            }

            switch (entityEntry.State)
            {
                case EntityState.Added:
                    entityEntry.Entity.AddCreation(user);
                    break;
                case EntityState.Modified:
                    entityEntry.Entity.AddModification(user);
                    break;
            }
        }
    }

    private void UpdateEntitiesState()
    {
        IEnumerable<EntityEntry<IEntity>> entries =
            _dbContext
                .ChangeTracker
                .Entries<IEntity>()
                .ToList();

        foreach (EntityEntry<IEntity> entityEntry in entries)
        {
            if (entityEntry.Entity.New)
            {
                entityEntry.State = EntityState.Added;
            }
            if (entityEntry.Entity.Deleted)
            {
                entityEntry.State = EntityState.Deleted;
            }
        }
    }
}
