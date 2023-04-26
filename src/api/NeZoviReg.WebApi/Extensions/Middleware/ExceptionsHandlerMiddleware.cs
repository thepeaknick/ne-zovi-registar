using System.Net;
using System.Text.Json;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.WebApi.Extensions.Middleware;

public class ExceptionsHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionsHandlingMiddleware> _logger;

    public ExceptionsHandlingMiddleware(ILogger<ExceptionsHandlingMiddleware> logger) =>
        _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(WebApiExtensions.CreateProblemDetails("Greška na serveru.",
                (int)HttpStatusCode.InternalServerError,
                RegErrors.App.InternalServerError
                ));

            await context.Response.WriteAsync(json);
        }
    }
}