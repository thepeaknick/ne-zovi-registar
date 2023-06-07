using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NeZoviReg.Abstractions.Email;

namespace NeZoviReg.Application.Email;

public class EmailSender :IEmailSender
{
    private readonly IOptionsSnapshot<EmailSenderOptions> _options;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(ILogger<EmailSender> logger, IOptionsSnapshot<EmailSenderOptions> options)
    {
        _logger = logger;
        _options = options;
    }

    public async Task<bool> SendEmailAsync(MailMessage message, CancellationToken cancellationToken)
    {
        if (!_options.Value.EmailEnabled)
        {
            _logger.LogDebug("EmailSender is disabled");
            return false;
        }

        LogMessage(message);

        message.To.Add(new MailAddress(_options.Value.EmailTo));

        using var client = SmtpClient;

        await client.SendMailAsync(message, cancellationToken);

        _logger.LogDebug("Email sent");

        return true;

    }

    private SmtpClient SmtpClient
    {
        get
        {
            var client = new SmtpClient(_options.Value.SmtpServer, _options.Value.Port);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_options.Value.Username, _options.Value.Password);

            return client;
        }
    }

    private void LogMessage(MailMessage message)
    {
        if (!_logger.IsEnabled(LogLevel.Debug))
            return;

        var to = string.Join(",", message.To.Select(x => x.Address));

        _logger.LogDebug($"Sending email to:{to}; subject:{message.Subject}", to, message.Subject);
    }

}