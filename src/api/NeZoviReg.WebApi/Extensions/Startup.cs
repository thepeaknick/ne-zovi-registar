using System.Net;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Mvc;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.WebApi.Extensions.Middleware;
using NeZoviReg.WebApi.Extensions.Options;
using NeZoviReg.WebApi.Extensions.WebApi;

namespace NeZoviReg.WebApi.Extensions;

public static class Startup
{
    public static IServiceCollection ConfigureWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers()
            .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); ;

        return services
            .AddEndpointsApiExplorer()
            .ConfigureOptions<AppOptionsSetup>()
            .AddDocumentation(configuration)
            .AddOptions()
            .AddTransient<ExceptionsHandlingMiddleware>()
            .AddRateLimiter(configuration)
            .AddApiVersioning();

    }

    private static IServiceCollection AddDocumentation(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettings = configuration
            .GetSection(AppOptions.SectionName)
            .Get<AppOptions>();

        return services.AddOpenApiDocument(c =>
            {
                c.DocumentName = $"v{appSettings?.Version}";
                c.GenerateEnumMappingDescription = true;
                c.Version = appSettings?.Version;
                c.Description = appSettings?.Description;
                c.Title = appSettings?.Title;
            });
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
                options.GroupNameFormat = "VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiVersion.Default;
            });

        return services;
    }
}