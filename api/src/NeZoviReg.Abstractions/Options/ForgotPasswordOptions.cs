using System.ComponentModel.DataAnnotations;

namespace NeZoviReg.Abstractions.Options;

public class ForgotPasswordOptions
{
    public const string SectionName = "ForgotPassword";
    
    public int TokenExpirationInMinutes { get; init; } = 1440; //24 hours
    
    [Required]
    public string? Subject { get; set; }
    
    [Required]
    public string? HtmlTemplatePath { get; set; }
    
    [Required]
    public string? CallBackUrl { get; set; }
}