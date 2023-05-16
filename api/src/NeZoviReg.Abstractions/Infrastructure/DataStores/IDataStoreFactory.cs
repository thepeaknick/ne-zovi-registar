namespace NeZoviReg.Abstractions.Infrastructure.DataStores;

public interface IDataStoreFactory
{
    TDataStore DataStore<TDataStore>() where TDataStore : IDataStore;
}