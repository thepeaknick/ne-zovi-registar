namespace NeZoviReg.Abstractions.Exceptions;

public class NeZoviRegAppException : Exception
{
    public NeZoviRegAppException(string message)
        : base(message)
    {
    }

    public NeZoviRegAppException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}