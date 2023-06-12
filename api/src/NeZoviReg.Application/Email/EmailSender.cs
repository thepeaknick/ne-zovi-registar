using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NeZoviReg.Abstractions.Email;
using NeZoviReg.Abstractions.Options;

namespace NeZoviReg.Application.Email;

public class EmailSender :IEmailSender
{
    private readonly EmailSenderOptions _options;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(ILogger<EmailSender> logger, IOptions<EmailSenderOptions> options)
    {
        _logger = logger;
        _options = options.Value;
    }

    public async Task<bool> SendEmailAsync(MailMessage message, CancellationToken cancellationToken)
    {
        if (!_options.EmailEnabled)
        {
            _logger.LogDebug("EmailSender is disabled");
            return false;
        }

        LogMessage(message);

        message.To.Add(new MailAddress(_options.EmailTo));

        using var client = SmtpClient;

        await client.SendMailAsync(message, cancellationToken);

        _logger.LogDebug("Email sent");

        return true;

    }

    private SmtpClient SmtpClient
    {
        get
        {
            var client = new SmtpClient(_options.SmtpServer, _options.Port);
            client.EnableSsl = _options.EnableSsl;
            client.UseDefaultCredentials = false;
            if(!string.IsNullOrEmpty(_options.Username) && !string.IsNullOrEmpty(_options.Password))
                client.Credentials = new NetworkCredential(_options.Username, _options.Password);

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