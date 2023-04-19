using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeZoviReg.Abstractions.Infrastructure;
using NeZoviReg.Auth.Infrastructure;
using NeZoviReg.Persistence.Ef.DataStores;
using NeZoviReg.Persistence.Ef.DataStores.Auth;

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

    private static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDataStoreFactory, DataStoreFactory>();
        services.AddScoped<IAuthDataStore, AuthDataStore>();

        return services;
    }
}