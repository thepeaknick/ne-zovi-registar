using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Application.Extensions;
using NeZoviReg.Auth.Extensions;
using NeZoviReg.Persistence.Ef.Extensions;

namespace NeZoviReg.Composition;

public static class Startup
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddAppAbstractions(configuration)
            .AddCors()
            .ConfigureAuth()
            .ConfigureAppCore(configuration)
            .ConfigurePersistence(configuration);
    }
}