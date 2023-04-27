namespace NeZoviReg.WebApi.Extensions.Options;

public class AppOptions
{
    public static string SectionName = @"Application";

    public string Name { get; init; }

    public string Title { get; init; }

    public string Version { get; init; }
}