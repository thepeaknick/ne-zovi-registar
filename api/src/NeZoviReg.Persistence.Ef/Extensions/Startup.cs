using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Persistence.Ef.DataStores;
using NeZoviReg.Persistence.Ef.DataStores.Auth;
using NeZoviReg.Persistence.Ef.DataStores.Domain;

namespace NeZoviReg.Persistence.Ef.Extensions;

public static class Startup
{
    public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<NeZoviRegDataContext>(op =>
            {
                op.ConfigureWarnings(x => x.Ignore(RelationalEventId.MultipleCollectionIncludeWarning));
                op.UseMySql(configuration.GetConnectionString("MySqlDatabase"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("MySqlDatabase")))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
                /*op.UseSqlServer(configuration.GetConnectionString("SqlServerDatabase"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);*/
            })
            .AddPersistenceServices(configuration);
    }

    private static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddSingleton<IDataStoreFactory, DataStoreFactory>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddDataStores(configuration);
    }

    private static IServiceCollection AddDataStores(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddScoped<IAuthDataStore, AuthDataStore>()
                .AddScoped<IRegUserDataStore, RegUserDataStore>()
                .AddScoped<IRegUserAccountDataStore, RegUserAccountDataStore>()
                .AddScoped<IUserDataStore, UserDataStore>();

    }
}