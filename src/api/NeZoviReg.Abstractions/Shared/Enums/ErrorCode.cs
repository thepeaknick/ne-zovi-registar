namespace NeZoviReg.Abstractions.Shared.Enums;

public enum ErrorCode
{
    Unknown = 0,

    None,

    Empty,

    EmailAlreadyInUse,

    NullValue,

    NotFound,

    InvalidCredentials,

    TooLong,

    InvalidFormat,

    ValidationError,

    Rejected,

}