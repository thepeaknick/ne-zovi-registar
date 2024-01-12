using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

#pragma warning disable CS8613

namespace NeZoviReg.Auth.Authorization;

public class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
    {
        FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }

    public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

    public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith(HasPermissionAttribute.PolicyPrefix))
        {
            var permission = policyName.Substring(HasPermissionAttribute.PolicyPrefix.Length);

            var policy = new AuthorizationPolicyBuilder()
                //.RequireAuthenticatedUser()
                .AddRequirements(new PermissionRequirement(permission))
                .Build();

            return Task.FromResult(policy);
        }

        return FallbackPolicyProvider.GetPolicyAsync(policyName)!;
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

    public async Task<AuthorizationPolicy> GetFallbackPolicyAsync() => await FallbackPolicyProvider.GetDefaultPolicyAsync();
}