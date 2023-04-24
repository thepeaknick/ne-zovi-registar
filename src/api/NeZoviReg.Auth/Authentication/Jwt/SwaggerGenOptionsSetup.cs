using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NeZoviReg.Auth.Authentication.Jwt;

public class SwaggerGenOptionsSetup : IPostConfigureOptions<SwaggerGenOptions>
{
    public void PostConfigure(string? name, SwaggerGenOptions options)
    {
        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            Scheme = "bearer",
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
            { jwtSecurityScheme, Array.Empty<string>() }
        });
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "NeZoviReg.Api", Version = "v1"});

        var assembly = Assembly.GetExecutingAssembly();
        var filePath = Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml");
        if (File.Exists(filePath))
        {
            options.IncludeXmlComments(filePath);
        }
    }
}