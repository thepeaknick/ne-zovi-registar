using FluentValidation;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Shared.Errors;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;

namespace NeZoviReg.Application.Services.RegUser;

public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    public SendEmailCommandValidator()
    {
        RuleFor(x => x.EmailFrom)!
            .NotEmpty<SendEmailCommand, string, bool>(RegErrors.Email.Empty.Message)
            .RegexFormat<SendEmailCommand, bool>(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegErrors.Email.InvalidFormat.Message);

        RuleFor(x => x.PhoneNumber)!
            .NotEmpty<SendEmailCommand, string, bool>(PhoneNumber.Empty.Message)
            .MaximumLength<SendEmailCommand, bool>(Domain.Model.Domain.User.PhoneNumberMaxLength, PhoneNumber.TooLong.Message)
            .RegexFormat<SendEmailCommand, bool>(@"^3816[0-9]{1}[0-9]{6,7}$", PhoneNumber.InvalidFormat.Message);

        When(x => string.IsNullOrEmpty(x.CompanyName), () =>
        {
            RuleFor(x => x.FirstName)!
                .NotEmpty<SendEmailCommand, string, bool>(FirstName.Empty.Message);

            RuleFor(x => x.LastName)!
                .NotEmpty<SendEmailCommand, string, bool>(LastName.Empty.Message);
        });
    }
}