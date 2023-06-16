using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NeZoviReg.WebApi.Extensions.Options;

public class SwaggerGenOptionsSetup : IPostConfigureOptions<SwaggerGenOptions>
{
    private readonly AppOptions _appOptions;
    private readonly XmlDocOptions _xmlDocOptions;
    
    public SwaggerGenOptionsSetup(IOptions<AppOptions> appOptions, IOptions<XmlDocOptions> xmlDocOptions)
    {
        _appOptions = appOptions.Value;
        _xmlDocOptions = xmlDocOptions.Value;
    }

    public void PostConfigure(string? name, SwaggerGenOptions options)
    {
        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            BearerFormat = "JWT",
            Name = "JWT Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Description = "Upiši samo JWT Bearer token u polje ispod!",

            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };
        options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {jwtSecurityScheme, Array.Empty<string>()}
        });
        options.SwaggerDoc($"v{_appOptions.Version}", new OpenApiInfo
        {
            Title = _appOptions.Title,
            Version = $"v{_appOptions.Version}",
            Description = _appOptions.Description,
        });

        foreach (var assembly in _xmlDocOptions.Assemblies)
        {
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{assembly}.xml"));
        }
    }
}