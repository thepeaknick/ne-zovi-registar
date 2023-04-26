using FluentValidation;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Messaging.Auth.Commands;

namespace NeZoviReg.Auth.Services.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty<LoginCommand, string, string>(Email.Empty.Message)
            .MaximumLength<LoginCommand, string>(Domain.Model.Domain.RegUser.EmailMaxLength, Email.TooLong.Message)
            .RegexFormat<LoginCommand, string>(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", Email.InvalidFormat.Message);
    }
}