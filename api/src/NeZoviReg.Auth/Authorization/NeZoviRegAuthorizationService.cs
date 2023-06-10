using System.Diagnostics.Eventing.Reader;
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
        var regUserId = user
            .Claims
            .SingleOrDefault(x => x.Type == CustomClaims.RegUserId)?.Value;

        if (!Guid.TryParse(regUserId, out var id))
        {
            _logger.LogWarning($"ClaimsPrincipal.Claims.RegUser={regUserId} is not integer.");

            return false;
        }

        if (!Enum.TryParse(typeof(PermissionType), permission, out var enumPermission))
        {
            _logger.LogWarning($"Permission={permission} is not of the PermissionType.");

            return false;
        }

        var regUserWithPermissions = await GetCachedUserWithPermissions(id, cancellationToken);

        bool WithPermission(PermissionType perm)
        {
            return (regUserWithPermissions.Permissions ?? new()).Any(permissionType => (permissionType & perm) == permissionType);
        }

       return regUserWithPermissions.RegUser.RefreshToken is not null && WithPermission((PermissionType)enumPermission);
    }

    private async Task<RegUserWithPermissions> GetCachedUserWithPermissions(Guid regUserId, CancellationToken cancellationToken = default)
    {
        return await _cache.GetAsync(CacheKeyPrefix.RegUser, regUserId,
           async () => await _authDataStore.GetUserWithPermissionsAsync(regUserId, cancellationToken),
           cancellationToken)
            .ConfigureAwait(false);
    }
}