using System.Text;
using Microsoft.Extensions.Options;
using NeZoviReg.Abstractions.Email;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Options;
using NeZoviReg.Abstractions.Shared;
using Serilog;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class SendEmailCommandHandler : ICommandHandler<SendEmailCommand, bool>
{
    private readonly IEmailSender _emailSender;
    private readonly EmailSenderOptions _options;
    public SendEmailCommandHandler(IEmailSender emailSender, IOptionsSnapshot<EmailSenderOptions> options)
    {
        _emailSender = emailSender;
        _options = options.Value;
    }
    
    public async Task<Result<bool>> Handle(SendEmailCommand command, CancellationToken cancellationToken)
    {
        var subject = CreateEmailSubject(command);

        return await _emailSender.SendEmailAsync(/*command.EmailFrom!, */_options.EmailTo, subject, command.Content!,
            cancellationToken: cancellationToken);
    }


    private string CreateEmailSubject(SendEmailCommand command)
    {
        var sb = new StringBuilder();

        var name =
            $"Naziv: {(command.FirstName != default ? $"{command.FirstName} {command.LastName}" : command.CompanyName)}";

        sb.AppendFormat($"{name}, Broj telefona:{command.PhoneNumber}");

        Log.Information($"Created email subject={sb}");

        return sb.ToString();
    }
}