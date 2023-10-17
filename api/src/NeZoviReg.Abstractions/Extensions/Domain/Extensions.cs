namespace NeZoviReg.Abstractions.Extensions.Domain;

public static class Extensions
{
    private const string SrbCode = "381";
    private const string SrbCodeWithPlus = "+381";
    
    public static string FormatPhoneNumber(this string phoneNumber)
    {
        if (phoneNumber.StartsWith("06"))
        {
            return phoneNumber.Substring(1, phoneNumber.Length - 1).Insert(0, SrbCode);
        }

        if (phoneNumber.StartsWith("6"))
        {
            return phoneNumber.Insert(0, SrbCode);
        }

        return phoneNumber.StartsWith(SrbCodeWithPlus) 
            ? phoneNumber.Substring(1, phoneNumber.Length - 1) 
            : phoneNumber;
    }
}