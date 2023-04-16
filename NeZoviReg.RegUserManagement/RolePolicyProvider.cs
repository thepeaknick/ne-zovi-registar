using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using NeZoviReg.Auth.Model.Enum;
#pragma warning disable CS8613

namespace NeZoviReg.Auth;

public class RolePolicyProvider : IAuthorizationPolicyProvider
{
    public RolePolicyProvider(IOptions<AuthorizationOptions> options)
    {
        FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }

    public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

    public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith(RoleAuthorizationAttribute.PolicyPrefix))
        {
            var role = policyName.Substring(RoleAuthorizationAttribute.PolicyPrefix.Length);

            var policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new RoleRequirement(Enum.Parse<RoleType>(role)))
                .Build();

            return Task.FromResult<AuthorizationPolicy>(policy);
        }

        return FallbackPolicyProvider.GetPolicyAsync(policyName)!;
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

    public async Task<AuthorizationPolicy> GetFallbackPolicyAsync() => await FallbackPolicyProvider.GetDefaultPolicyAsync();
}