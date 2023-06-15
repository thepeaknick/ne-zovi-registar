using Microsoft.Extensions.Options;

namespace NeZoviReg.WebApi.Extensions.Options;

public class XmlDocOptionsSetup : IConfigureOptions<XmlDocOptions>
{
    private const string sectionName = @"XmlDoc";

    private readonly IConfiguration _configuration;

    public XmlDocOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(XmlDocOptions options)
    {
        _configuration
            .GetSection(sectionName)
            .Bind(options);
    }
}