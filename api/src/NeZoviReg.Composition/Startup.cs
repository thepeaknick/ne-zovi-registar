using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Application.Extensions;
using NeZoviReg.Auth.Extensions;
using NeZoviReg.Persistence.Ef.Extensions;
using NeZoviReg.WebClient;

namespace NeZoviReg.Composition;

public static class Startup
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddAppAbstractions(configuration)
            .ConfigureAuth(configuration)
            .ConfigureAppCore(configuration)
            .ConfigurePersistence(configuration)
            .ConfigureWebClients(configuration)
            .ConfigureAppAutoMapper(configuration);
    }


    private static IServiceCollection ConfigureAppAutoMapper(this IServiceCollection services, IConfiguration configuration)
    {
        var config = new MapperConfiguration(c =>
        {
            AppCoreAutoMapperExtension.AddMappingProfiles.Invoke(c);
            WebClientAutoMapperExtension.AddMappingProfiles.Invoke(c);
            
        });

        return services
            .AddSingleton<AutoMapper.IConfigurationProvider>(config)
            .AddSingleton(config.CreateMapper());
    }
}