using Microsoft.Extensions.Options;

namespace NeZoviReg.WebApi.Extensions.Options;

public class AppOptionsSetup : IConfigureOptions<AppOptions>
{
    private const string sectionName = @"Application";

    private readonly IConfiguration _configuration;

    public AppOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(AppOptions options)
    {
        _configuration
            .GetSection(sectionName)
            .Bind(options);
    }
}