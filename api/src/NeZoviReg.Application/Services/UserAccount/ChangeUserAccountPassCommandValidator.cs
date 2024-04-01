using FluentValidation;
using NeZoviReg.Abstractions.Extensions;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.UserAccount;

public class ChangeUserAccountPassCommandValidator : AbstractValidator<ChangePassCommand>
{
    public ChangeUserAccountPassCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty<ChangePassCommand, string, bool>(RegErrors.UserAccount.NotLoggedIn.Message);

        RuleFor(x => x.NewPassword)
            .NotEmpty<ChangePassCommand, string, bool>(Password.Empty.Message)
            .MaximumLength<ChangePassCommand, bool>(Domain.Model.Auth.RegUserAccount.PasswordMaxLength,
                Password.TooLong.Message);
    }
}