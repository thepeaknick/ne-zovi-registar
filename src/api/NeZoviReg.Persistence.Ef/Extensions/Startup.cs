using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeZoviReg.Abstractions.Infrastructure;
using NeZoviReg.Application.Infrastructure.DataStores;
using NeZoviReg.Auth.Infrastructure;
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
            op.UseSqlServer(configuration.GetConnectionString("SqlServerDatabase"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
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
                .AddScoped<IUserDataStore, UserDataStore>();

    }
}