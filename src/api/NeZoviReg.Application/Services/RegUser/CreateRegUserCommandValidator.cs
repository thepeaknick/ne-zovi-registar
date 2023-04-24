using FluentValidation;
using NeZoviReg.Abstractions.Shared.Enums;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Application.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Extensions;
using static NeZoviReg.Abstractions.Shared.Errors.ValidationErrors;

namespace NeZoviReg.Application.Services.RegUser;

public class CreateRegUserCommandValidator : AbstractValidator<CreateRegUserCommand>
{
    public CreateRegUserCommandValidator(IRegUserDataStore regUserDataStore)
    {
        RuleFor(x => x.Email)
            .NotEmpty<CreateRegUserCommand, string, string>(Email.Empty.Message)
            .MaximumLength<CreateRegUserCommand, string>(Domain.Model.Domain.RegUser.EmailMaxLength,
                Email.TooLong.Message);
        RuleFor(x => x.UserName)
            .NotEmpty<CreateRegUserCommand, string, string>(ValidationErrors.RegUser.UserNameTooLong.Message)
            .MaximumLength<CreateRegUserCommand, string>(Domain.Model.Domain.RegUser.UsernameMaxLength,
                ValidationErrors.RegUser.UserNameTooLong.Message);

        RuleFor(x => x.Password)
            .NotEmpty<CreateRegUserCommand, string, string>(ValidationErrors.RegUser.PasswordTooLong.Message)
            .MaximumLength<CreateRegUserCommand, string>(Domain.Model.Domain.RegUser.PasswordMaxLength,
                ValidationErrors.RegUser.PasswordTooLong.Message);

        RuleFor(x => x.Email).MustAsync((email, cancellationToken) => regUserDataStore.IsEmailUniqueAsync(email, cancellationToken))
        .WithErrorCode(ErrorCode.EmailAlreadyInUse.ToString())
        .WithMessage(x => ValidationErrors.RegUser.EmailAlreadyInUse(x.Email).Message);
    }
}