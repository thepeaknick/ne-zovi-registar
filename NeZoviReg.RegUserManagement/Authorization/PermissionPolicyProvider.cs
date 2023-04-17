using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using NeZoviReg.Auth.Model;
using NeZoviReg.Auth.Model.Enum;
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
        if (policyName.StartsWith(PermissionAuthorizationAttribute.PolicyPrefix))
        {
            var permission = policyName.Substring(PermissionAuthorizationAttribute.PolicyPrefix.Length);

            var policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(permission))
                .Build();

            return Task.FromResult<AuthorizationPolicy>(policy);
        }

        return FallbackPolicyProvider.GetPolicyAsync(policyName)!;
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

    public async Task<AuthorizationPolicy> GetFallbackPolicyAsync() => await FallbackPolicyProvider.GetDefaultPolicyAsync();
}