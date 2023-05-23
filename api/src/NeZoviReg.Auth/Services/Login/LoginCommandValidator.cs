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
            .NotEmpty<LoginCommand, string, LoginResultDto>(RegUser.InvalidCredentials.Message)
            .MaximumLength<LoginCommand, LoginResultDto>(Domain.Model.Domain.RegUser.UsernameMaxLength, UserName.TooLong.Message);

        RuleFor(x => x.Password)
            .NotEmpty<LoginCommand, string, LoginResultDto>(RegUser.InvalidCredentials.Message)
            .MaximumLength<LoginCommand, LoginResultDto>(Domain.Model.Domain.RegUser.PasswordMaxLength, Password.TooLong.Message);
    }
}