using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Auth.Authorization;
using NeZoviReg.WebApi.Extensions.WebApi;

namespace NeZoviReg.WebApi.Extensions.Middleware;

public class NeZoviAuthorizationMiddleware: IAuthorizationMiddlewareResultHandler
{
    private readonly ILogger<NeZoviAuthorizationMiddleware> _logger;
    private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

    public NeZoviAuthorizationMiddleware(ILogger<NeZoviAuthorizationMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync(
        RequestDelegate next,
        HttpContext context,
        AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Forbidden && authorizeResult.AuthorizationFailure!.FailureReasons.FirstOrDefault()?.Handler is PermissionRequirementHandler)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            var json = JsonSerializer.Serialize(WebApiExtensions.CreateProblemDetails("Autorizacija neuspešna.",
                (int)HttpStatusCode.Forbidden,
                RegErrors.App.ForbiddenAccess
            ));
            await context.Response.WriteAsync(json);
        }
        else
        {
            // Fall back to the default implementation.
            await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}

