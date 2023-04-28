using Microsoft.EntityFrameworkCore;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Persistence.Ef.DataStores.Domain;

public class UserDataStore : IUserDataStore
{
    private readonly NeZoviRegDataContext _dbContext;

    public UserDataStore(NeZoviRegDataContext dbcontext)
    {
        _dbContext = dbcontext;
    }

    public async Task<User?> GetByPhoneNumber(string phoneNumber, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);

    public async Task<List<User>> GetAll(DateTime? after, CancellationToken cancellationToken = default)
        => await _dbContext.Set<User>().Where(x => x.CreatedOn >= (after ?? DateTime.MinValue)).ToListAsync(cancellationToken);

    public async Task AddAsync(User user, CancellationToken cancellationToken = default) =>
       await _dbContext.Set<User>().AddAsync(user, cancellationToken);

    public void Update(User user) =>
        _dbContext.Set<User>().Update(user);

    public void Remove(User user) =>
        _dbContext.Set<User>().Remove(user);
}