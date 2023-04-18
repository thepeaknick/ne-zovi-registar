using NeZoviReg.Domain.Shared.Enums;

namespace NeZoviReg.Domain.Shared;

public interface IValidationResult
{
    public static readonly Error ValidationError = new(
        ErrorCode.ValidationError,
        "Došlo je do problema sa validacijom.");

    Error[] Errors { get; }
}
