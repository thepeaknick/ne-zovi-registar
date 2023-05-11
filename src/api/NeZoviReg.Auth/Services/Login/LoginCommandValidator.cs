using FluentValidation;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Messaging.Auth.Commands;

namespace NeZoviReg.Auth.Services.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty<LoginCommand, string, string>(RegUser.InvalidCredentials.Message)
            .MaximumLength<LoginCommand, string>(Domain.Model.Domain.RegUser.UsernameMaxLength, UserName.TooLong.Message);

        RuleFor(x => x.Password)
            .NotEmpty<LoginCommand, string, string>(RegUser.InvalidCredentials.Message)
            .MaximumLength<LoginCommand, string>(Domain.Model.Domain.RegUser.PasswordMaxLength, Password.TooLong.Message);
    }
}