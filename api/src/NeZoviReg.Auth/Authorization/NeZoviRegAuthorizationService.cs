using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Abstractions.Shared.Caching;
using NeZoviReg.Abstractions.Shared.Model.Auth;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;

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
        var userAccountId = user
            .Claims
            .SingleOrDefault(x => x.Type == CustomClaims.UserId)?.Value;
        
        var regUsrId = user
            .Claims
            .SingleOrDefault(x => x.Type == CustomClaims.RegUserId)?.Value;

        if (!Guid.TryParse(userAccountId, out var userId))
        {
            _logger.LogWarning($"ClaimsPrincipal.Claims.UserAccountId={userAccountId} is not Guid.");

            return false;
        }
        
        if (!Guid.TryParse(regUsrId, out var regUserId))
        {
            _logger.LogWarning($"ClaimsPrincipal.Claims.RegUserId={regUsrId} is not Guid.");

            return false;
        }

        if (!Enum.TryParse(typeof(PermissionType), permission, out var enumPermission))
        {
            _logger.LogWarning($"Permission={permission} is not of the PermissionType.");

            return false;
        }

        var userAccountWithPermissions = await GetCachedUserWithPermissions(regUserId, cancellationToken);

        bool WithPermission(PermissionType perm)
        {
            return (userAccountWithPermissions.Permissions ?? new()).Any(permissionType => (permissionType & perm) == permissionType);
        }

       return userAccountWithPermissions.UserAccount(userId).AccessTokenExpirationTime is not null && WithPermission((PermissionType)enumPermission);
    }

    private async Task<UserAccountWithPermissions> GetCachedUserWithPermissions(Guid regUserId, CancellationToken cancellationToken = default)
    {
        return await _cache.GetAsync(CacheKeyPrefix.RegUser, regUserId,
           async () => await _authDataStore.GetUserWithPermissionsAsync(regUserId, cancellationToken),
           cancellationToken).ConfigureAwait(false);
    }
}