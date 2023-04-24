using NeZoviReg.Abstractions.Shared.Enums;

namespace NeZoviReg.Abstractions.Extensions;

public static class AbstractionsExtensions
{
    public static string ErrorCodeToString(this ErrorCode code)
    {
        return code.ToString();
    }
}