using FluentValidation;
using static NeZoviReg.Abstractions.Shared.Errors.ValidationErrors;
using NeZoviReg.Abstractions.Extensions;

namespace NeZoviReg.Auth.Services.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty<LoginCommand, string, string>(Email.Empty.Message)
            .MaximumLength<LoginCommand, string>(Domain.Model.Domain.RegUser.EmailMaxLength, Email.TooLong.Message)
            .InvalidFormat<LoginCommand, string, string>(Email.InvalidFormat.Message, email => email.Split('@').Length == 2);
    }
}