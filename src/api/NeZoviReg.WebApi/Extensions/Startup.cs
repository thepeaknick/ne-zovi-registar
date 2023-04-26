using System.Net;
using System.Threading.RateLimiting;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.WebApi.Extensions.Options;

namespace NeZoviReg.WebApi.Extensions;

public static class Startup
{
    public static IServiceCollection ConfigureWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        return services
                .AddRateLimiter(configuration);
    }

    private static IServiceCollection AddRateLimiter(this IServiceCollection services, IConfiguration configuration)
    {
        var rlOptions = new FixedWindowRateLimitOptions();
        configuration
            .GetSection(FixedWindowRateLimitOptions.SectionName)
            .Bind(rlOptions);

        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    httpContext.Request.Headers.Host.ToString(),
                    _ => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = rlOptions.PermitLimit,
                        QueueLimit = rlOptions.QueueLimit,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        Window = TimeSpan.FromMinutes(rlOptions.WindowInMinutes)
                    }));

            options.OnRejected = async (context, cancellationToken) =>
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;

                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    await context.HttpContext.Response.WriteAsync(ValidationErrors.App.RateLimitRejected(retryAfter.Minutes).Message, cancellationToken);
                }
                else
                {
                    await context.HttpContext.Response.WriteAsync(ValidationErrors.App.RateLimitRejected(default).Message, cancellationToken);
                }

            };
        });
        return services;
    }
}