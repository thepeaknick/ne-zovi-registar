using Microsoft.EntityFrameworkCore;
using NeZoviReg.Application.Infrastructure.DataStores;
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

    public void Add(RegUser regUser) =>
        _dbContext.Set<RegUser>().Add(regUser);

    public void Update(RegUser regUser)=>
        _dbContext.Set<RegUser>().Update(regUser);
}