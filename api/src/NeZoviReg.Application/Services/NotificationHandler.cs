using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NeZoviReg.Abstractions.Email;
using NeZoviReg.Abstractions.Options;
using NeZoviReg.Abstractions.Shared.Events;

namespace NeZoviReg.Application.Services;

internal class NotificationHandler : INotificationHandler<RegUserCreatedEvent>
{
    private readonly ILogger<NotificationHandler> _logger;
    private readonly IEmailSender _emailSender;
    private readonly EmailSenderOptions _options;
    
    public NotificationHandler(IEmailSender emailSender,
        IOptionsSnapshot<EmailSenderOptions> options,
        ILogger<NotificationHandler> logger)
    {
        _emailSender = emailSender;
        _options = options.Value;
       _logger = logger;
    }

    public async Task Handle(RegUserCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"RegUserCreatedEvent Name={notification.CompanyName} published.");
        
        var result = await _emailSender.SendEmailAsync(_options.EmailTo,
            $"Trgovac: {notification.CompanyName} je registrovan.",
            $"Korisničko ime: {notification.Username}",
            cancellationToken: cancellationToken);

        _logger.LogInformation(result
            ? $"Notification to the Email={notification.Email} sent successfully."
            : $"Notification to the Email={notification.Email} failed to be sent.");
    }
}
