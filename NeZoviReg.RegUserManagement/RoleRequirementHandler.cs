using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace NeZoviReg.Auth;

public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
{
    private readonly IServiceProvider _serviceProvider;

    public RoleRequirementHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        var authService = _serviceProvider.GetRequiredService<INeZoviRegAuthorizationService>();
        if (await authService.HasRole(context.User, requirement.Role))
            context.Succeed(requirement);
    }
}