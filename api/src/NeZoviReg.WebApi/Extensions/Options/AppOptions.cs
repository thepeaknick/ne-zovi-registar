namespace NeZoviReg.WebApi.Extensions.Options;

public class AppOptions
{
    public static string SectionName = @"Application";

    public string Name { get; init; } = string.Empty;

    public string Title { get; init; } = string.Empty;

    public string Version { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;
}