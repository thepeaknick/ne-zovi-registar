using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace NeZoviReg.WebApi.Extensions.Middleware;

public class ExceptionsHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionsHandlingMiddleware> _logger;

    public ExceptionsHandlingMiddleware(
        ILogger<ExceptionsHandlingMiddleware> logger) =>
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

            context.Response.StatusCode =
                (int)HttpStatusCode.InternalServerError;

            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Greška na serveru.",
                Title = "Greška na serveru.",
                Detail = "Desila se greška na serveru."
            };

            var json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}