using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace NeZoviReg.WebApi.Extensions.Options;

public class SwaggerUiOptionsSetup : IConfigureOptions<SwaggerUIOptions>
{
    private readonly IServiceProvider _serviceProvider;

    public SwaggerUiOptionsSetup(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Configure(SwaggerUIOptions options)
    {
        var descriptionProvider = _serviceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

        // Build a swagger endpoint for each discovered API version
        foreach (var description in descriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"{description.GroupName}/swagger.json", description.GroupName);
        }
    }
}