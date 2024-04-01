using Microsoft.EntityFrameworkCore;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Domain.Extensions;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Persistence.Ef.DataStores.Domain;

public class RegUserDataStore : IRegUserDataStore
{
    private readonly NeZoviRegDataContext _dbContext;

    public RegUserDataStore(NeZoviRegDataContext dbcontext)
    {
        _dbContext = dbcontext;
    }

    public async Task<bool> IsRegNumberExistsAsync(string regNumber, Guid? excludeId = default,
        CancellationToken cancellationToken = default)
        => await _dbContext
            .Set<RegUser>()
            .AnyAsync(
                regUser => regUser.RegNumber == regNumber && (excludeId == default || regUser.GuidId != excludeId),
                cancellationToken);

    public async Task<bool> IsTaxNumberExistsAsync(string taxNumber, Guid? excludeId = default,
        CancellationToken cancellationToken = default)
        => await _dbContext
            .Set<RegUser>()
            .AnyAsync(
                regUser => regUser.TaxNumber == taxNumber && (excludeId == default || regUser.GuidId != excludeId),
                cancellationToken);

    public async Task<bool> IsCompanyNameExistsAsync(string name, Guid? excludeId = default,
        CancellationToken cancellationToken = default)
        => await _dbContext
            .Set<RegUser>()
            .AnyAsync(regUser => regUser.CompanyName == name && (excludeId == default || regUser.GuidId != excludeId),
                cancellationToken);

    public async Task<bool> IsEmailExistsAsync(string email, Guid? excludeId = default,
        CancellationToken cancellationToken = default)
        => await _dbContext
            .Set<RegUser>()
            .AnyAsync(regUser => regUser.Email == email && (excludeId == default || regUser.GuidId != excludeId),
                cancellationToken);

    public async Task<RegUser?> GetByGuidId(Guid regUserId, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<RegUser>()
            .Include(u => u.RegUserRoles)
            .SingleOrDefaultAsync(x => x.GuidId == regUserId, cancellationToken);

    public async Task<bool> Exists(Guid regUserId, CancellationToken cancellationToken = default) =>
    await _dbContext.Set<RegUser>()
        .SingleOrDefaultAsync(x => x.GuidId == regUserId, cancellationToken) != default;

    public async Task<RegUser?> GetWithAccountsByGuidId(Guid regUserId, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<RegUser>()
            .Include(u => u.UserAccounts)
            .SingleOrDefaultAsync(x => x.GuidId == regUserId, cancellationToken);

   public async Task<RegUser?> GetById(int id, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<RegUser>()
            .Include(u => u.RegUserRoles)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<RegUser?> GetByEmail(string email, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<RegUser>()
            .SingleOrDefaultAsync(x => x.Email == email, cancellationToken);

    public async Task<List<RegUser>> GetByRole(RoleType role, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<RegUser>()
            .Include(u => u.RegUserRoles)
            .Where(x => x.RegUserRoles.Select(r => r.RoleId).Contains((int) role))
            .ToListAsync(cancellationToken);

    public async Task Add(RegUser regUser, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<RegUser>().AddAsync(regUser, cancellationToken);

    public void Update(RegUser regUser) =>
        _dbContext.Set<RegUser>().Update(regUser);

    public void Remove(RegUser regUser) =>
        _dbContext.Set<RegUser>().Remove(regUser);
}