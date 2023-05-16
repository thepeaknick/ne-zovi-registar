using FluentValidation;
using NeZoviReg.Abstractions.Extensions;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.RegUser;

public class ChangePassRegUserCommandValidator : AbstractValidator<ChangePassRegUserCommand>
{
    public ChangePassRegUserCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty<ChangePassRegUserCommand, string, RegUserDto>(RegErrors.RegUser.NotLoggedIn.Message);

        RuleFor(x => x.NewPassword)
            .NotEmpty<ChangePassRegUserCommand, string, RegUserDto>(Password.Empty.Message)
            .MaximumLength<ChangePassRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.PasswordMaxLength,
                Password.TooLong.Message);
    }
}