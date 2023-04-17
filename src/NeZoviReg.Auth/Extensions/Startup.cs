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
                //options.ChainTrustValidationMode = X509ChainTrustMode.CustomRootTrust;
                options.Events = new CertificateAuthenticationEvents
                {
                    OnCertificateValidated = context =>
                    {
                        var validationService =
                            context.HttpContext.RequestServices.GetService<ICertValidationService>();

                        var userId = validationService?.ValidateCertificateWithUserId(context.ClientCertificate);
                        if (userId != default)
                        {
                            var claims = new[]
                            {
                                new Claim(ClaimTypes.NameIdentifier, userId.ToString()!)
                            };

                            context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));
                            context.Success();
                        }
                        else
                        {
                            context.Fail("Nevalidan sertifikat!");
                        }

                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        context.Fail("Nevalidan sertifikat!");
                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }

}