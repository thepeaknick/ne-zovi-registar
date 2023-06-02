using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Model.Auth;

namespace NeZoviReg.Auth.Authentication.Cert;

public class NeZoviRegCertAuthenticationOptionsSetup : IPostConfigureOptions<CertificateAuthenticationOptions>
{
    public void PostConfigure(string? name, CertificateAuthenticationOptions options)
    {
        options.AllowedCertificateTypes = CertificateTypes.SelfSigned;
        options.Events = new CertificateAuthenticationEvents
        {
            OnCertificateValidated = async context =>
            {
                var validationService = context.HttpContext.RequestServices.GetService<ICertValidationService>();

                var regUser = await validationService?.ValidateCertificate(context.ClientCertificate)!;

                if (regUser is not null)
                {
                    var claims = new List<Claim>
                    {
                        new(CustomClaims.RegUserId, regUser.GuidId.ToString())
                    };

                    context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));
                    context.Success();
                }
                else
                {
                    context.Fail(Result.Failure<string>(RegErrors.RegUser.NotRegistered).Error.Message);
                }
            },
            OnAuthenticationFailed = context =>
            {
                context.Fail(Result.Failure<string>(RegErrors.RegUser.NotRegistered).Error.Message);
                return Task.CompletedTask;
            }
        };
    }

}