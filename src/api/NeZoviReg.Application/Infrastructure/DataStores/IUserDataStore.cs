using NeZoviReg.Domain.Model.Domain;

namespace NeZoviReg.Application.Infrastructure.DataStores;

public interface IUserDataStore
{
    void Add(User regUser);

    void Update(User member);
}