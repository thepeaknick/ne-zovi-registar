using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeZoviReg.Abstractions.Shared.Caching;

namespace NeZoviReg.Abstractions.Extensions;

public static class Startup
{
    public static IServiceCollection AddAppAbstractions(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSingleton<ICacheService, CacheService>();
    }
}