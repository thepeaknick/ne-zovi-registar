using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeZoviReg.Abstractions.Behaviors;

namespace NeZoviReg.Application.Extensions;

public static class Startup
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        return services
            .AddMediatR(typeof(Startup).Assembly)
            .AddApplicationServices(configuration);

    }

    private static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {

        return services;
    }
}