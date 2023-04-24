namespace NeZoviReg.Abstractions.Infrastructure;

public interface IDataStoreFactory
{
    TDataStore DataStore<TDataStore>() where TDataStore : IDataStore;
}