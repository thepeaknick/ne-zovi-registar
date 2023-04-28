using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NeZoviReg.Auth.Authentication.Jwt;
using NeZoviReg.Auth.Authentication.Services;

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
            OnCertificateValidated = async context =>
            {
                var validationService = context.HttpContext.RequestServices.GetService<ICertValidationService>();

                var regUser = await validationService?.ValidateCertificate(context.ClientCertificate)!;

                if (regUser.HasValue)
                {
                    var claims = new List<Claim>
                    {
                        new(CustomClaims.RegUserId, regUser.Value.ToString())
                    };

                    context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));
                    context.Success();
                }
                else
                {
                    context.Fail("Invalid certificate");
                }
            },
            OnAuthenticationFailed = context =>
            {
                context.Fail("Invalid certificate");
                return Task.CompletedTask;
            }
        };
    }

}