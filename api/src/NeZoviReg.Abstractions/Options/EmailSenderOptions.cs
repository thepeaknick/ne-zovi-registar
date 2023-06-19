using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618

namespace NeZoviReg.Abstractions.Options;

public class EmailSenderOptions
{
    public const string SectionName = "Email";

    [Required]
    public string SmtpServer { get; set; }

    [Required]
    public int Port { get; set; }

    [Required]
    public string EmailTo { get; set; }

    public bool EnableSsl { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public bool EmailEnabled { get; set; }
}
