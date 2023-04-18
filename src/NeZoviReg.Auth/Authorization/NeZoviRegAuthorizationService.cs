using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NeZoviReg.Auth.Authentication.Services;
using NeZoviReg.Auth.Infrastructure;

namespace NeZoviReg.Auth.Authorization;

public class NeZoviRegAuthorizationService : DefaultAuthorizationService, INeZoviRegAuthorizationService
{
    private readonly IAuthDataStore _authDataStore;
    private readonly ILogger<NeZoviRegAuthorizationService> _logger;
    public NeZoviRegAuthorizationService(IAuthorizationPolicyProvider policyProvider,
        IAuthorizationHandlerProvider handlers,
        ILogger<NeZoviRegAuthorizationService> logger,
        IAuthorizationHandlerContextFactory contextFactory,
        IAuthorizationEvaluator evaluator,
        IOptions<AuthorizationOptions> options,
        IAuthDataStore authDataStore)
        : base(policyProvider, handlers, logger, contextFactory, evaluator,
        options)
    {
        _authDataStore = authDataStore;
        _logger = logger;
    }

    public async Task<bool> HasPermission(ClaimsPrincipal user, string permission, CancellationToken cancellationToken = default)
    {
        var regUserId = user
            .Claims
            .SingleOrDefault(x => x.Type == CustomClaims.RegUserId)?.Value;

        if (!int.TryParse(regUserId, out var regId))
        {
            _logger.LogWarning($"ClaimsPrincipal.Claims.RegUser={regUserId} is not integer.");

            return false;
        }

        var permissions = await _authDataStore.GetUserPermissionsAsync(regId, cancellationToken);

        //TODO Cache

        return permissions.Contains(permission);
    }
}