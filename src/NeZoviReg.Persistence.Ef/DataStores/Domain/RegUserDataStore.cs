using Microsoft.EntityFrameworkCore;
using NeZoviReg.Application.Infrastructure;
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
}