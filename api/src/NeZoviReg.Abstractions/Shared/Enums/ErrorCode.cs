namespace NeZoviReg.Abstractions.Shared.Enums;

public enum ErrorCode
{
    Unknown = 0,

    None,

    Empty,

    AlreadyInUse,

    NullValue,

    NotFound,

    NoContent,

    InvalidCredentials,

    TooLong,

    InvalidFormat,

    ValidationError,

    Rejected,

    InternalServerError,

    Forbidden,
}