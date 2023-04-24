using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeZoviReg.Application.Extensions;
using NeZoviReg.Auth.Extensions;
using NeZoviReg.Persistence.Ef.Extensions;

namespace NeZoviReg.Composition;

public static class Startup
{
    public static IServiceCollection ConfigureNeZoviRegApp(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .ConfigureAuth()
            .ConfigureApplication(configuration)
            .ConfigurePersistence(configuration);
    }
}