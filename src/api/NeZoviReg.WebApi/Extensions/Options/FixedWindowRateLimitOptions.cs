namespace NeZoviReg.WebApi.Extensions.Options;

public class FixedWindowRateLimitOptions
{
    public static string SectionName = @"RateLimit:FixedWindow";

    public int PermitLimit { get; init; }

    public int WindowInMinutes { get; init; }

    public int QueueLimit { get; init; }
}