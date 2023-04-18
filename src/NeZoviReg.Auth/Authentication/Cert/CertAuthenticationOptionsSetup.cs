using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NeZoviReg.Auth.Authentication.Jwt;

namespace NeZoviReg.Auth.Authentication.Cert;

public class NeZoviRegCertAuthenticationOptionsSetup : IPostConfigureOptions<CertificateAuthenticationOptions>
{
    private readonly JwtOptions _options;

    public NeZoviRegCertAuthenticationOptionsSetup(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public void PostConfigure(string? name, CertificateAuthenticationOptions options)
    {
        options.AllowedCertificateTypes = CertificateTypes.SelfSigned;
        options.Events = new CertificateAuthenticationEvents
        {
            OnCertificateValidated = context =>
            {
                var validationService = context.HttpContext.RequestServices.GetService<ICertValidationService>();

                if (validationService?.ValidateCertificate(context.ClientCertificate) ?? false)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, context.ClientCertificate.Subject, ClaimValueTypes.String,
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
    }

}