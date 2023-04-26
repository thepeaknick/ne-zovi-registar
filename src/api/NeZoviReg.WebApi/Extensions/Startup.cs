using System.Net;
using System.Threading.RateLimiting;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.WebApi.Extensions.Middleware;
using NeZoviReg.WebApi.Extensions.Options;
using NeZoviReg.WebApi.Extensions.WebApi;

namespace NeZoviReg.WebApi.Extensions;

public static class Startup
{
    public static IServiceCollection ConfigureWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        return services
            .AddEndpointsApiExplorer()
            .AddSwagger(configuration)
            .AddOptions()
            .AddTransient<ExceptionsHandlingMiddleware>()
            .AddRateLimiter(configuration)
            .AddApiVersioning();

    }

    private static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSwaggerGen();
    }

    private static IServiceCollection AddRateLimiter(this IServiceCollection services, IConfiguration configuration)
    {
        var fixedWindowRateLimitOptions = new FixedWindowRateLimitOptions();
        configuration
            .GetSection(FixedWindowRateLimitOptions.SectionName)
            .Bind(fixedWindowRateLimitOptions);

        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    httpContext.Request.Headers.Host.ToString(),
                    _ => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = fixedWindowRateLimitOptions.PermitLimit,
                        QueueLimit = fixedWindowRateLimitOptions.QueueLimit,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        Window = TimeSpan.FromSeconds(fixedWindowRateLimitOptions.WindowInSeconds)
                    }));

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
        return services;
    }

    private static IServiceCollection AddApiVersioning(this IServiceCollection services)
    {
        services.AddControllers(o =>
        {
            o.UseGeneralRoutePrefix("/v{version:apiVersion}");
        });

        services.AddApiVersioning(o =>
        {
            o.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(
            options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";
                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }
}