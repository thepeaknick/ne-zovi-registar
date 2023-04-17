using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NeZoviReg.Persistence.Ef.Extensions;

public static class Startup
{
    public static IServiceCollection ConfigureDataStore(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            /*.AddDbContext<SqlLiteDbContext>(op =>
            {
                op.UseSqlite(configuration.GetConnectionString("SqlLiteDatabase"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });*/
            .AddDbContext<SqlServerDbContext>(op =>
        {
            op.UseSqlite(configuration.GetConnectionString("SqlServerDatabase"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }
}