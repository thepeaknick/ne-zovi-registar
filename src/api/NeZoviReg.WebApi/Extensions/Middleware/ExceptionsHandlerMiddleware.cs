namespace NeZoviReg.WebApi.Extensions.Middleware;

public class ExceptionsHandlerMiddlaware : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        throw new NotImplementedException();
    }
}