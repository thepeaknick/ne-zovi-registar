using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeZoviReg.Abstractions.Infrastructure.WebClient;
using NeZoviReg.WebClient.Apr;
using NeZoviReg.WebClient.Apr.Options;

namespace NeZoviReg.WebClient.Extensions;

public static class WebClientExtensions
{
    public static IServiceCollection ConfigureWebClients(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<AprWebClientOptions>()
            .Bind(configuration.GetSection(AprWebClientOptions.SectionName))
            .Validate(x => !string.IsNullOrWhiteSpace(x.BaseUrl), $"BaseUrl not defined {nameof(AprWebClientOptions)}")
            .Validate(x => !string.IsNullOrWhiteSpace(x.CertificateSerialNumber), $"Cert serial number not defined for {nameof(AprWebClientOptions)}")
            .Validate(x => !string.IsNullOrWhiteSpace(x.Credentials.Username), $"Username not defined for {nameof(AprWebClientOptions)}.{nameof(AprWebClientOptions.Credentials)}")
            .Validate(x => !string.IsNullOrWhiteSpace(x.Credentials.Password), $"Password not defined for {nameof(AprWebClientOptions)}.{nameof(AprWebClientOptions.Credentials)}");

        return services.AddScoped<IAprWebClient, AprSoapWebClient>();
    }
}