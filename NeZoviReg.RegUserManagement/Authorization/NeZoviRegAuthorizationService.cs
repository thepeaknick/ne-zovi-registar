using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NeZoviReg.Auth.Model.Enum;

namespace NeZoviReg.Auth.Authorization;

public class NeZoviRegAuthorizationService : DefaultAuthorizationService, INeZoviRegAuthorizationService
{
    public NeZoviRegAuthorizationService(IAuthorizationPolicyProvider policyProvider,
        IAuthorizationHandlerProvider handlers,
        ILogger<DefaultAuthorizationService> logger,
        IAuthorizationHandlerContextFactory contextFactory,
        IAuthorizationEvaluator evaluator,
        IOptions<AuthorizationOptions> options)
        : base(policyProvider, handlers, logger, contextFactory, evaluator, options)
    {
    }

    public Task<bool> HasPermission(ClaimsPrincipal user, string permission, CancellationToken token = default)
    {
        //read from Db and cache each user permissions
        return Task.FromResult(true);
    }
}