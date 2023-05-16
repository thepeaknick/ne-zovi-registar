using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;

namespace NeZoviReg.Abstractions.Extensions;

public static class AbstractionsExtensions
{
    public static List<int>? ToIntList(this List<RoleType>? roles)
    {
        return roles?.Select(r=>(int)r).ToList() ?? default;
    }
}