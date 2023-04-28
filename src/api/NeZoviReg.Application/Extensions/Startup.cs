using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeZoviReg.Abstractions.Behaviors;

namespace NeZoviReg.Application.Extensions;

public static class Startup
{
    public static IServiceCollection ConfigureAppCore(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddMediatR(typeof(Startup).Assembly)
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
            .AddValidatorsFromAssembly(typeof(Startup).Assembly)
            .AddAutoMapper()
            .AddApplicationServices(configuration);

    }

    private static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }

    private static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        var config = new MapperConfiguration(c =>
        {
            AutoMapperExtension.AddApplicationProfile.Invoke(c);
        });

        return services
            .AddSingleton<AutoMapper.IConfigurationProvider>(config)
            .AddSingleton(config.CreateMapper());
    }
}