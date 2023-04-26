namespace NeZoviReg.Abstractions.Exceptions;

public class NeZoviRegAppException : Exception
{
    protected NeZoviRegAppException(string message)
        : base(message)
    {
    }
}