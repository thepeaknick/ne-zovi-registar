using System.ComponentModel.DataAnnotations;

namespace NeZoviReg.Auth.Services.Login;

public class ForgotPasswordOptions
{
    public const string SectionName = "ForgotPassword";
    
    [Required]
    public string? SmtpServer { get; set; }

    [Required]
    public int Port { get; set; }

    [Required]
    public string? EmailFrom { get; set; }

    [Required]
    public string? Subject { get; set; }
    
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }
    
    [Required]
    public string? HtmlTemplatePath { get; set; }
    
    [Required]
    public string? CallBackUrl { get; set; }
}