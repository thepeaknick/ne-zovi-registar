using System.Text;

namespace NeZoviReg.Domain.Extensions;

public static class Helpers
{
    public static string Encode(this string value) => Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

    public static string Decode(this string value) => Encoding.UTF8.GetString(Convert.FromBase64String(value));
}