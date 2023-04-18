using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using NeZoviReg.Auth.Authentication.Services;

namespace NeZoviReg.Auth.Authorization;

public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceProvider _serviceProvider;

    public PermissionRequirementHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        using var scope = _serviceProvider.CreateScope();
        var authService = scope.ServiceProvider.GetRequiredService<INeZoviRegAuthorizationService>();

        if(await authService.HasPermission(context.User, requirement.Permission, CancellationToken.None))
            context.Succeed(requirement);

        /*var permissions = context
            .User
            .Claims
            .Where(x => x.Type == CustomClaims.Permissions)
            .Select(x => x.Value)
            .ToHashSet();

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }*/
    }


}