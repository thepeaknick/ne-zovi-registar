using FluentValidation;
using MediatR;
using NeZoviReg.Abstractions.Shared;

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

        //var errors = new List<Error>();
        var errorsDictionary = new Dictionary<string, string[]>();
        foreach (var validator in _validators)
        {
            var res = await validator.ValidateAsync(request, cancellationToken);
            var filteredErrors = res.Errors.Where(e => e is not null).ToList();

            /*errors.AddRange(filteredErrors.Select(e => Enum.TryParse(e.ErrorCode, out ErrorCode code)
                    ? new Error(code, e.ErrorMessage) :
                    new Error(e.PropertyName, e.ErrorMessage))
                .Distinct()
                .ToList());*/

            errorsDictionary = filteredErrors
                .GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values);
        }
        /*if (errors.Any())
        {
            return CreateValidationResult<TResponse>(errors.ToArray());
        }*/
        if (errorsDictionary.Any())
        {
            return CreateValidationResult<TResponse>(errorsDictionary);
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

    private static TResult CreateValidationResult<TResult>(Dictionary<string, string[]> errors)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
        {
            return (ValidationResult.WithErrorsDict(errors) as TResult)!;
        }

        object validationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrorsDict))!
            .Invoke(null, new object?[] { errors })!;

        return (TResult)validationResult;
    }
}
