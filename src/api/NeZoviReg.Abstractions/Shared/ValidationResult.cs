namespace NeZoviReg.Abstractions.Shared;

public sealed class ValidationResult : Result, IValidationResult
{
    private ValidationResult(Error[] errors)
        : base(false, IValidationResult.ValidationError) =>
        Errors = errors;

    private ValidationResult(Dictionary<string, string[]> errors)
        : base(false, IValidationResult.ValidationError) =>
        ErrorsDictionary = errors;

    public Error[] Errors { get; }

    public Dictionary<string, string[]> ErrorsDictionary { get; }

    public static ValidationResult WithErrors(Error[] errors) => new(errors);

    public static ValidationResult WithErrorsDict(Dictionary<string, string[]> errors) => new(errors);
}
