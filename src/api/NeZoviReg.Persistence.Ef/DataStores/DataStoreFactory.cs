using Microsoft.Extensions.DependencyInjection;
using NeZoviReg.Abstractions.Infrastructure;

namespace NeZoviReg.Persistence.Ef.DataStores;

public class DataStoreFactory : IDataStoreFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DataStoreFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TDataStore DataStore<TDataStore>() where TDataStore : IDataStore
    {
        TDataStore dataStore = _serviceProvider.GetRequiredService<TDataStore>();

        return dataStore;
    }
}