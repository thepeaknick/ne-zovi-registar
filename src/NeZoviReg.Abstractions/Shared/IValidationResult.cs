using NeZoviReg.Abstractions.Shared.Enums;

namespace NeZoviReg.Abstractions.Shared;

public interface IValidationResult
{
    public static readonly Error ValidationError = new(
        ErrorCode.ValidationError,
        "Došlo je do problema sa validacijom.");

    Error[] Errors { get; }
}
