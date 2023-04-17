using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.Extensions.DependencyInjection;
using NeZoviReg.Auth.Authentication;

namespace NeZoviReg.Auth.Extensions;

public static class Startup
{
    public static IServiceCollection AddNeZoviRegAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
            .AddCertificate(options =>
            {
                options.AllowedCertificateTypes = CertificateTypes.SelfSigned;
                options.Events = new CertificateAuthenticationEvents
                {
                    OnCertificateValidated = context =>
                    {
                        var validationService =
                            context.HttpContext.RequestServices.GetService<ICertValidationService>();
                        if (validationService.ValidateCertificate(context.ClientCertificate))
                        {
                            var claims = new[]
                            {
                                new Claim(ClaimTypes.NameIdentifier, context.ClientCertificate.Subject,
                                    ClaimValueTypes.String,
                                    context.Options.ClaimsIssuer),
                                new Claim(ClaimTypes.Name, context.ClientCertificate.Subject, ClaimValueTypes.String,
                                    context.Options.ClaimsIssuer)
                            };

                            context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));
                            context.Success();
                        }
                        else
                        {
                            context.Fail("Invalid certificate");
                        }

                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        context.Fail("Invalid certificate");
                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }

}