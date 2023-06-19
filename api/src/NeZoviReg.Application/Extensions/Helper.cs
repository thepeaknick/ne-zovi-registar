namespace NeZoviReg.Application.Extensions;

public static class Helper
{
    public static string ToNeZoviString(this DateTime? dt)
    {
        return $"{dt:dd.MM.yyyy HH:MM}";
    }
}