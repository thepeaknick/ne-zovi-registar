namespace NeZoviReg.WebApi.Extensions.Options;

public class XmlDocOptions
{
    public const string SectionName = @"XmlDoc";

    public string[] Assemblies { get; init; }

}