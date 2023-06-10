using FluentValidation;
using NeZoviReg.Abstractions.Extensions;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.RegUser;

public class ChangePassRegUserCommandValidator : AbstractValidator<ChangePassCommand>
{
    public ChangePassRegUserCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty<ChangePassCommand, string, bool>(RegErrors.RegUser.NotLoggedIn.Message);

        RuleFor(x => x.NewPassword)
            .NotEmpty<ChangePassCommand, string, bool>(Password.Empty.Message)
            .MaximumLength<ChangePassCommand, bool>(Domain.Model.Domain.RegUser.PasswordMaxLength,
                Password.TooLong.Message);
    }
}