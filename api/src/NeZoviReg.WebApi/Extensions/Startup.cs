using System.Net;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Model;
using NeZoviReg.WebApi.Extensions.Middleware;
using NeZoviReg.WebApi.Extensions.Options;
using NeZoviReg.WebApi.Extensions.WebApi;
using NeZoviReg.WebApi.Infrastructure;

namespace NeZoviReg.WebApi.Extensions;

public static class Startup
{
    public static IServiceCollection ConfigureWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddHttpContextAccessor()
            .AddEndpointsApiExplorer()
            .AddCors()
            .AddApiDocumentation()
            .AddOptions()
            .AddRateLimiter(configuration)
            .ConfigureOptions<AppOptionsSetup>()
            .ConfigureOptions<XmlDocOptionsSetup>()
            .ConfigureExceptionHandling(configuration)
            //.AddSingleton<IAuthorizationMiddlewareResultHandler, NeZoviAuthorizationMiddleware>()
            .AddApiVersioning()
            .AddCors();

    }

    public static IConfigurationBuilder AddConfigurationJsonFiles(this IConfigurationBuilder builder, IHostEnvironment? environment = null, bool reloadOnChange = true)
    {
        builder.AddJsonFile("appsettings.json", false, reloadOnChange);
        if (environment != null)
            builder.AddJsonFile("appsettings." + environment.EnvironmentName + ".json", true, reloadOnChange);
        return builder.AddJsonFile("appsettings.my.json", true, false);
    }

    private static IServiceCollection AddRateLimiter(this IServiceCollection services, IConfiguration configuration)
    {
        var fixedWindowRateLimitOptionsAnonymous = new FixedWindowRateLimitOptions();
        configuration
            .GetSection(FixedWindowRateLimitOptions.SectionNameAnonymous)
            .Bind(fixedWindowRateLimitOptionsAnonymous);
        
        var fixedWindowRateLimitOptionsAuthenticated = new FixedWindowRateLimitOptions();
        configuration
            .GetSection(FixedWindowRateLimitOptions.SectionNameAuthenticated)
            .Bind(fixedWindowRateLimitOptionsAuthenticated);

        return services.AddRateLimiter(options =>
        {
            options.AddPolicy(Const.AnonymousLogin, httpContext =>
            
                RateLimitPartition.GetFixedWindowLimiter(httpContext.Connection.RemoteIpAddress?.ToString() ?? httpContext.Request.Headers.Host.ToString(),
                    _ => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = fixedWindowRateLimitOptionsAnonymous.PermitLimit,
                        QueueLimit = fixedWindowRateLimitOptionsAnonymous.QueueLimit,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        Window = TimeSpan.FromSeconds(fixedWindowRateLimitOptionsAnonymous.WindowInSeconds)
                    }));
            
            options.AddPolicy(Const.AuthenticatedLogin, httpContext =>
            
                
                RateLimitPartition.GetFixedWindowLimiter(AppUser.GetUserName(httpContext.User.Identity) ?? httpContext.Request.Headers.Host.ToString(),
                    _ => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = fixedWindowRateLimitOptionsAuthenticated.PermitLimit,
                        QueueLimit = fixedWindowRateLimitOptionsAuthenticated.QueueLimit,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        Window = TimeSpan.FromSeconds(fixedWindowRateLimitOptionsAuthenticated.WindowInSeconds)
                    }));

            /*options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    httpContext.Connection.RemoteIpAddress?.ToString() ?? httpContext.Request.Headers.Host.ToString(),
                    _ => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = fixedWindowRateLimitOptions.PermitLimit,
                        QueueLimit = fixedWindowRateLimitOptions.QueueLimit,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        Window = TimeSpan.FromSeconds(fixedWindowRateLimitOptions.WindowInSeconds)
                    }));*/

            options.OnRejected = async (context, cancellationToken) =>
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;

                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    await context.HttpContext.Response.WriteAsync(RegErrors.App.RateLimitRejected(retryAfter.Minutes).Message, cancellationToken);
                }
                else
                {
                    await context.HttpContext.Response.WriteAsync(RegErrors.App.RateLimitRejected(default).Message, cancellationToken);
                }

            };
        });
    }

    private static IServiceCollection AddApiVersioning(this IServiceCollection services)
    {
        services.AddControllers(o =>
            {
                o.UseGeneralRoutePrefix("/v{version:apiVersion}");
            })
            .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddApiVersioning(o =>
        {
            o.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiVersion.Default;
            });

        return services;
    }

    private static IServiceCollection AddApiDocumentation(this IServiceCollection services)
    {
        return services
            .AddSwaggerGen()
            .ConfigureOptions<SwaggerGenOptionsSetup>()
            .ConfigureOptions<SwaggerUiOptionsSetup>();
    }

    private static IServiceCollection ConfigureExceptionHandling(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddTransient<NeZoviExceptionsHandlingMiddleware>();
    }

    private static IServiceCollection AddCors(this IServiceCollection services)
    {
        return services.AddCors(options =>
        {
            options.AddPolicy("any",
                policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }
}