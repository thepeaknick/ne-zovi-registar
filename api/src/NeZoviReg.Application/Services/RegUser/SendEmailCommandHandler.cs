using System.Text;
using NeZoviReg.Abstractions.Email;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Shared;
using Serilog;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class SendEmailCommandHandler : ICommandHandler<SendEmailCommand, bool>
{
    private readonly IEmailSender _emailSender;
    private readonly IUnitOfWork _unitOfWork;

    public SendEmailCommandHandler(IUnitOfWork unitOfWork, IEmailSender emailSender)
    {
        _unitOfWork = unitOfWork;
        _emailSender = emailSender;
    }

    public async Task<Result<bool>> Handle(SendEmailCommand command, CancellationToken cancellationToken)
    {
        var subject = CreateEmailSubject(command);

        return await _emailSender.SendEmailAsync(command.EmailFrom!, subject, command.Content!,
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