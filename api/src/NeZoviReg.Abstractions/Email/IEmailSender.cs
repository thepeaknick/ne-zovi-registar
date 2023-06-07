using System.Net.Mail;

namespace NeZoviReg.Abstractions.Email;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(MailMessage message, CancellationToken token);
}