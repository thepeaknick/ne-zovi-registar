using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NeZoviReg.Auth.Authentication.Cert;
using NeZoviReg.Auth.Authentication.Jwt;
using NeZoviReg.Auth.Authorization;
using NeZoviReg.Auth.Services.Login;

namespace NeZoviReg.Auth.Extensions;

public static class Startup
{
    public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            //.AddNeZoviRegCertAuthentication()
            .AddNeZoviRegJwtAuthentication()
            .AddAuthorizationServices()
            .AddNeZoviRegLoginServices(configuration)
            .AddMemoryCache()
            .AddMediatR(typeof(Startup).Assembly)
            .AddValidatorsFromAssembly(typeof(Startup).Assembly);


    }

    private static IServiceCollection AddNeZoviRegCertAuthentication(this IServiceCollection services)
    {
        services.ConfigureOptions<NeZoviRegCertAuthenticationOptionsSetup>();
        services.AddScoped<ICertValidationService, CertValidationService>();
        services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
            .AddCertificate();

        return services;
    }

    private static IServiceCollection AddNeZoviRegJwtAuthentication(this IServiceCollection services)
    {
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.AddScoped<IJwtProvider, JwtProvider>();
        
       

        return services;
    }

    private static IServiceCollection AddNeZoviRegLoginServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ForgotPasswordOptions>()
            .Bind(configuration.GetSection(ForgotPasswordOptions.SectionName))
            .ValidateDataAnnotations();

        return services;
    }


    private static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<INeZoviRegAuthorizationService, NeZoviRegAuthorizationService>();

        return services;
    }
}
