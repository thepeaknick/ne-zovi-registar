using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Abstractions.Shared.Caching;
using NeZoviReg.Auth.Authentication.Services;

namespace NeZoviReg.Auth.Authorization;

public class NeZoviRegAuthorizationService : DefaultAuthorizationService, INeZoviRegAuthorizationService
{
    private readonly IAuthDataStore _authDataStore;
    private readonly ILogger<NeZoviRegAuthorizationService> _logger;
    private readonly ICacheService _cache;

    public NeZoviRegAuthorizationService(IAuthorizationPolicyProvider policyProvider,
        IAuthorizationHandlerProvider handlers,
        ILogger<NeZoviRegAuthorizationService> logger,
        IAuthorizationHandlerContextFactory contextFactory,
        IAuthorizationEvaluator evaluator,
        IOptions<AuthorizationOptions> options,
        IAuthDataStore authDataStore,
        ICacheService cache)
        : base(policyProvider, handlers, logger, contextFactory, evaluator,
        options)
    {
        _authDataStore = authDataStore;
        _cache = cache;
        _logger = logger;
    }

    public async Task<bool> HasPermission(ClaimsPrincipal user, string permission, CancellationToken cancellationToken = default)
    {
        var regUserId = user
            .Claims
            .SingleOrDefault(x => x.Type == CustomClaims.RegUserId)?.Value;

        if (!Guid.TryParse(regUserId, out var id))
        {
            _logger.LogWarning($"ClaimsPrincipal.Claims.RegUser={regUserId} is not integer.");

            return false;
        }

        var permissions = await GetCachedUserPermissions(id, cancellationToken);

       return permissions?.Contains(permission) ?? false;
    }

    private async Task<List<string>?> GetCachedUserPermissions(Guid regUserId, CancellationToken cancellationToken = default)
    {
        return await _cache.GetAsync(CacheKeyPrefix.RegUser, regUserId,
           async () => await _authDataStore.GetUserPermissionsAsync(regUserId, cancellationToken),
           cancellationToken)
            .ConfigureAwait(false);
    }
}