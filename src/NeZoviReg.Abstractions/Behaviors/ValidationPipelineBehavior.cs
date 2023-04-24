using FluentValidation;
using MediatR;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Enums;

namespace NeZoviReg.Abstractions.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) =>
        _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var errors = new List<Error>();

        foreach (var validator in _validators)
        {
            var res = await validator.ValidateAsync(request, cancellationToken);
            errors.AddRange(res.Errors.Where(e => e is not null)
                .Select(failure => new Error(
                    Enum.TryParse(failure.ErrorCode, out ErrorCode code) ? code : ErrorCode.Unknown,
                    failure.ErrorMessage))
                .Distinct()
                .ToList());
        }
        if (errors.Any())
        {
            return CreateValidationResult<TResponse>(errors.ToArray());
        }

        return await next();
    }

    private static TResult CreateValidationResult<TResult>(Error[] errors)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
        {
            return (ValidationResult.WithErrors(errors) as TResult)!;
        }

        object validationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, new object?[] { errors })!;

        return (TResult)validationResult;
    }
}
