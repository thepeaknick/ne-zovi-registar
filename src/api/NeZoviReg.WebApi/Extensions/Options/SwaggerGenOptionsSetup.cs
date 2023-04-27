using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NeZoviReg.WebApi.Extensions.Options;

public class SwaggerGenOptionsSetup : IPostConfigureOptions<SwaggerGenOptions>
{
    private readonly AppOptions _appOptions;

    public SwaggerGenOptionsSetup(IOptions<AppOptions> appOptions)
    {
        _appOptions = appOptions.Value;
    }

    public void PostConfigure(string? name, SwaggerGenOptions options)
    {
        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            Scheme = "Bearer",
            BearerFormat = "JWT",
            Name = "JWT Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Description = "Put *_ONLY_* your JWT Bearer token on textbox below!",

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
            Version = _appOptions.Version,
            Description = _appOptions.Description,
        });

        var assembly = Assembly.GetExecutingAssembly();
        var filePath = Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml");
        if (File.Exists(filePath))
        {
            options.IncludeXmlComments(filePath);
        }
    }
}