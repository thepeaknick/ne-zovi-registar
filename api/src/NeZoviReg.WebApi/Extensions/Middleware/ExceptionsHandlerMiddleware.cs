using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Auth.Exceptions;
using Serilog;
using static NeZoviReg.WebApi.Extensions.WebApi.WebApiExtensions;
namespace NeZoviReg.WebApi.Extensions.Middleware;

public class NeZoviExceptionsHandlingMiddleware : IMiddleware
{
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (RefreshTokenExpiredException e)
        {
            await context.Response.WriteAsync(HandleException(context, e, CreateProblemDetails("Refresh token je istekao. Ulogujte se ponovo.",
                (int)HttpStatusCode.Unauthorized,
                RegErrors.App.ForbiddenAccess
            )));
        }
        catch (RefreshTokenEmptyException e)
        {
            await context.Response.WriteAsync(HandleException(context, e, CreateProblemDetails("Izlogovani ste iz sistema. Ulogujte se ponovo.",
                (int)HttpStatusCode.Unauthorized,
                RegErrors.App.ForbiddenAccess
            )));
        }
        catch (SecurityTokenInvalidSignatureException e)
        {
            await context.Response.WriteAsync(HandleException(context, e, CreateProblemDetails("Nevalidan potpis tokena. Algoritam nije ispravan.",
                (int)HttpStatusCode.Unauthorized,
                RegErrors.App.ForbiddenAccess
            )));
        }
        catch (SecurityTokenExpiredException e)
        {
            await context.Response.WriteAsync(HandleException(context, e, CreateProblemDetails("Nevalidan token.",
                (int)HttpStatusCode.Unauthorized,
                RegErrors.Token.AccessTokenExpired
            )));
        }
        catch (SecurityTokenException e)
        {
            await context.Response.WriteAsync(HandleException(context, e, CreateProblemDetails("Nevalidan token.",
                (int)HttpStatusCode.Unauthorized,
                RegErrors.App.ForbiddenAccess
            )));
        }
        catch (Exception e)
        {
           await context.Response.WriteAsync(HandleException(context, e));
        }
    }

    private string HandleException(HttpContext context, Exception e, ProblemDetails? pd = null)
    {
        pd ??= CreateProblemDetails("Greška na serveru.",
            (int) HttpStatusCode.InternalServerError,
            RegErrors.App.InternalServerError
        );

        Log.Fatal(e, e.Message);

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var json = JsonSerializer.Serialize(pd);

        return json;
    }
}