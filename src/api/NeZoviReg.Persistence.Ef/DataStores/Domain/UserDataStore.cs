using NeZoviReg.Application.Infrastructure.DataStores;
using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Persistence.Ef.DataStores.Domain;

public class UserDataStore : IUserDataStore
{
    private readonly NeZoviRegDataContext _dbContext;

    public UserDataStore(NeZoviRegDataContext dbcontext)
    {
        _dbContext = dbcontext;
    }

    public void Add(User regUser) =>
        _dbContext.Set<User>().Add(regUser);

    public void Update(User regUser)=>
        _dbContext.Set<User>().Update(regUser);
}