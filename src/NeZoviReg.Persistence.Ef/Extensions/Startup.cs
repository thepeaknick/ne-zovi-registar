using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NeZoviReg.Persistence.Ef.Extensions;

public static class Startup
{
    public static IServiceCollection ConfigureSqlLiteDataStore(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<SqlLiteDbContext>(op =>
            {
                op.UseSqlite(configuration.GetConnectionString("SqlLiteDatabase"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
    }
}