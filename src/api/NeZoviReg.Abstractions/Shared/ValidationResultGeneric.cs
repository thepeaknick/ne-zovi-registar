#pragma warning disable CS8618
namespace NeZoviReg.Abstractions.Shared;

public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    private ValidationResult(Error[] errors)
        : base(default, false, IValidationResult.ValidationError) =>
        Errors = errors;

    private ValidationResult(Dictionary<string, string[]> errors)
        : base(default, false, IValidationResult.ValidationError) =>
        ErrorsDictionary = errors;

    public Error[] Errors { get; }

    public Dictionary<string, string[]> ErrorsDictionary { get; }

    public static ValidationResult<TValue> WithErrorsDict(Dictionary<string, string[]> errors) => new(errors);

    public static ValidationResult<TValue> WithErrors(Error[] errors) => new(errors);
}
