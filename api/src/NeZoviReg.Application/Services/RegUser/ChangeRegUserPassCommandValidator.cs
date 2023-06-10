using FluentValidation;
using NeZoviReg.Abstractions.Extensions;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.RegUser;

public class ChangeRegUserPassCommandValidator : AbstractValidator<ChangePassCommand>
{
    public ChangeRegUserPassCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty<ChangePassCommand, string, bool>(RegErrors.RegUser.NotLoggedIn.Message);

        RuleFor(x => x.NewPassword)
            .NotEmpty<ChangePassCommand, string, bool>(Password.Empty.Message)
            .MaximumLength<ChangePassCommand, bool>(Domain.Model.Domain.RegUser.PasswordMaxLength,
                Password.TooLong.Message);
    }
}