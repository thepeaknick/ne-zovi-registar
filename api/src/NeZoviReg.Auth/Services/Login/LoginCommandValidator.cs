using FluentValidation;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Messaging.Auth.Commands;
using NeZoviReg.Abstractions.Messaging.Auth.Model;

namespace NeZoviReg.Auth.Services.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty<LoginCommand, string, LoginResultDto>(UserAccount.InvalidCredentials.Message)
            .MaximumLength<LoginCommand, LoginResultDto>(Domain.Model.Auth.RegUserAccount.UsernameMaxLength, UserName.TooLong.Message);

        RuleFor(x => x.Password)
            .NotEmpty<LoginCommand, string, LoginResultDto>(UserAccount.InvalidCredentials.Message)
            .MaximumLength<LoginCommand, LoginResultDto>(Domain.Model.Auth.RegUserAccount.PasswordMaxLength, Password.TooLong.Message);
    }
}