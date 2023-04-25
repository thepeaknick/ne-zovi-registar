using Microsoft.EntityFrameworkCore;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Domain.Model.Auth;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Persistence.Ef.DataStores.Domain;

public class RegUserDataStore : IRegUserDataStore
{
    private readonly NeZoviRegDataContext _dbContext;

    public RegUserDataStore(NeZoviRegDataContext dbcontext)
    {
        _dbContext = dbcontext;
    }

    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default)
        => !await _dbContext
            .Set<RegUser>()
            .AnyAsync(user => user.Email == email, cancellationToken);

    public async Task<bool> IsUsernamelUniqueAsync(string username, CancellationToken cancellationToken = default)
        => !await _dbContext
            .Set<RegUser>()
            .AnyAsync(user => user.Username == username, cancellationToken);

    public async Task<RegUser?> Get(Guid regUserId, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<RegUser>().SingleOrDefaultAsync(x => x.GuidId == regUserId, cancellationToken);

    public async Task Add(RegUser regUser, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<RegUser>().AddAsync(regUser, cancellationToken);

    public void Update(RegUser regUser)=>
        _dbContext.Set<RegUser>().Update(regUser);

    public void Remove(RegUser regUser) =>
        _dbContext.Set<RegUser>().Remove(regUser);

    public async Task AddRole(int regUserId, int roleId, CancellationToken cancellationToken = default)
        => await _dbContext.Set<RegUserRole>().AddAsync(new RegUserRole(regUserId, roleId), cancellationToken);

    public async Task AddRoles(int regUserId, List<int> roleIds, CancellationToken cancellationToken = default)
    {
        foreach (var roleId in roleIds)
        {
            await _dbContext.Set<RegUserRole>().AddAsync(new RegUserRole(regUserId, roleId), cancellationToken);
        }
    }
}