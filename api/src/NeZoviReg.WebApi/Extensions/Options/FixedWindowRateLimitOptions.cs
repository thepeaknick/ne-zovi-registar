namespace NeZoviReg.WebApi.Extensions.Options;

public class FixedWindowRateLimitOptions
{
    public static string SectionNameAnonymous = @"RateLimit:FixedWindowAnonymous";
    
    public static string SectionNameAuthenticated = @"RateLimit:FixedWindowAuthenticated";

    public int PermitLimit { get; init; }

    public int WindowInSeconds { get; init; }

    public int QueueLimit { get; init; }
}