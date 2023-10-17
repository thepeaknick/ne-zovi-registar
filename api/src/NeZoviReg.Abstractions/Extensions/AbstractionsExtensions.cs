using System.Text;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;

namespace NeZoviReg.Abstractions.Extensions;

public static class AbstractionsExtensions
{
    public static List<int>? ToIntList(this List<RoleType>? roles)
    {
        return roles?.Select(r=>(int)r).ToList() ?? default;
    }
    
    
    public static string RemoveWhitespaces(this string source)
    {
        var builder = new StringBuilder(source.Length);
        for (int i = 0; i < source.Length; i++)
        {
            char c = source[i];
            if (!char.IsWhiteSpace(c))
                builder.Append(c);
        }
        return source.Length == builder.Length ? source : builder.ToString();
    }
    
    public static string RemoveSpaces(this string source)
    {
        return source.Replace(" ", string.Empty);
    }
}