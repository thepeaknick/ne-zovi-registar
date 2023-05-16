using Microsoft.Extensions.Options;

namespace NeZoviReg.WebApi.Extensions.Options;

public class AppOptionsSetup : IConfigureOptions<AppOptions>
{
    public static string SectionName = @"Application";

    private readonly IConfiguration _configuration;

    public AppOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(AppOptions options)
    {
        _configuration
            .GetSection(SectionName)
            .Bind(options);
    }
}