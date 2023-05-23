using System.Net;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.WebApi.Extensions.WebApi;

namespace NeZoviReg.WebApi.Extensions.Middleware;

public class NeZoviExceptionsHandlingMiddleware : IMiddleware
{
    private readonly ILogger<NeZoviExceptionsHandlingMiddleware> _logger;

    public NeZoviExceptionsHandlingMiddleware(ILogger<NeZoviExceptionsHandlingMiddleware> logger) =>
        _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (SecurityTokenInvalidSignatureException e)
        {
            _logger.LogError(e, e.Message);

            context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(WebApiExtensions.CreateProblemDetails("Nevalidan potpis tokena. Algoritam nije ispravan.",
                (int) HttpStatusCode.Unauthorized,
                RegErrors.App.ForbiddenAccess
            ));

            await context.Response.WriteAsync(json);

        }

        catch (SecurityTokenException e)
        {
            _logger.LogError(e, e.Message);

            context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(WebApiExtensions.CreateProblemDetails("Nevalidan token.",
                (int) HttpStatusCode.Unauthorized,
                RegErrors.App.ForbiddenAccess
            ));

            await context.Response.WriteAsync(json);

        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(WebApiExtensions.CreateProblemDetails("Greška na serveru.",
                (int) HttpStatusCode.InternalServerError,
                RegErrors.App.InternalServerError
            ));

            await context.Response.WriteAsync(json);
        }
    }
}