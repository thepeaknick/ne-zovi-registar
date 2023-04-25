using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using NeZoviReg.Auth.Authentication.Services;
using NeZoviReg.Auth.Infrastructure;

namespace NeZoviReg.Auth.Authorization;

public class NeZoviRegAuthorizationService : DefaultAuthorizationService, INeZoviRegAuthorizationService
{
    private readonly IAuthDataStore _authDataStore;
    private readonly ILogger<NeZoviRegAuthorizationService> _logger;
    private readonly IMemoryCache _cache;
    private const string KeyTokenSource = "Permissions_TokenSource";

    public NeZoviRegAuthorizationService(IAuthorizationPolicyProvider policyProvider,
        IAuthorizationHandlerProvider handlers,
        ILogger<NeZoviRegAuthorizationService> logger,
        IAuthorizationHandlerContextFactory contextFactory,
        IAuthorizationEvaluator evaluator,
        IOptions<AuthorizationOptions> options,
        IAuthDataStore authDataStore,
        IMemoryCache cache)
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

       return permissions.Contains(permission);
    }

    private CancellationTokenSource GetTokenSource() => _cache.GetOrCreate(KeyTokenSource, _ => new CancellationTokenSource())!;

    private async Task<List<string>> GetCachedUserPermissions(Guid regUserId, CancellationToken cancellationToken)
    {
        return await _cache.GetOrCreateAsync($"reg_user_{regUserId}", async ce =>
            {
                ConfigureCacheEntry(ce, GetTokenSource());

                return await _authDataStore.GetUserPermissionsAsync(regUserId, cancellationToken)
                    .ConfigureAwait(false);
            })
            .ConfigureAwait(false) ?? new List<string>();
    }
    private static void ConfigureCacheEntry(ICacheEntry cacheEntry, CancellationTokenSource cts)
    {
        cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
        cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(10);
        cacheEntry.AddExpirationToken(new CancellationChangeToken(cts.Token));
    }
}