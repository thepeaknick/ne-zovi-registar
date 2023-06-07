using MediatR;
using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Shared;

namespace NeZoviReg.Abstractions.Behaviors;

public class LoggingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger) =>_logger = logger;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting request {@Request}, {@DateTime}", request, DateTime.Now);

        var result = await next();

        if (result.IsFailure)
        {
            _logger.LogError("Request failure {RequestName}, {@Error}, {@DateTime}", typeof(TRequest).Name, result.Error, DateTime.Now);
        }

        _logger.LogInformation("Completed request {RequestName}, {@DateTime}", typeof(TRequest).Name, DateTime.Now);
        return result;
    }
}
