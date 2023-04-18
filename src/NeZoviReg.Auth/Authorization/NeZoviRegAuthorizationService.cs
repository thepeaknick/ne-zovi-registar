using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NeZoviReg.Auth.Authentication.Services;

namespace NeZoviReg.Auth.Authorization;

public class NeZoviRegAuthorizationService : DefaultAuthorizationService, INeZoviRegAuthorizationService
{
    public NeZoviRegAuthorizationService(IAuthorizationPolicyProvider policyProvider,
        IAuthorizationHandlerProvider handlers, ILogger<DefaultAuthorizationService> logger,
        IAuthorizationHandlerContextFactory contextFactory, IAuthorizationEvaluator evaluator,
        IOptions<AuthorizationOptions> options) 
        : base(policyProvider, handlers, logger, contextFactory, evaluator,
        options)
    {
    }

    public Task<bool> HasPermission(ClaimsPrincipal user, string permission, CancellationToken token = default)
    {
        var regUserId = user
            .Claims
            .SingleOrDefault(x => x.Type == CustomClaims.RegUserId)?.Value;

        //2. get the user permissions and cache
        //3. check of the user has given requirement permission
        return Task.FromResult(true);
    }
}