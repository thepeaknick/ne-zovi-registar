using Microsoft.AspNetCore.Authorization;
using NeZoviReg.Auth.Authentication.Services;

namespace NeZoviReg.Auth.Authorization;

public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
{
   protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var permissions = context
            .User
            .Claims
            .Where(x => x.Type == CustomClaims.Permissions)
            .Select(x => x.Value)
            .ToHashSet();

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}