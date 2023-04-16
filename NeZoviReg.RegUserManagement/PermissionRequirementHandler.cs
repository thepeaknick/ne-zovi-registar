using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace NeZoviReg.Auth;

public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceProvider _serviceProvider;

    public PermissionRequirementHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var authService = _serviceProvider.GetRequiredService<INeZoviRegAuthorizationService>();
        if (await authService.HasPermission(context.User, requirement.Permission))
            context.Succeed(requirement);
    }
}