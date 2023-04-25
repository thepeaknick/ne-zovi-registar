using FluentValidation;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging.Domain.Commands;
using static NeZoviReg.Abstractions.Shared.Errors.ValidationErrors;

namespace NeZoviReg.Application.Services.RegUser;

public class CreateRegUserCommandValidator : AbstractValidator<CreateRegUserCommand>
{
    public CreateRegUserCommandValidator(IRegUserDataStore regUserDataStore)
    {
        RuleFor(x => x.Email)
            .NotEmpty<CreateRegUserCommand, string, string>(Email.Empty.Message)
            .MaximumLength<CreateRegUserCommand, string>(Domain.Model.Domain.RegUser.EmailMaxLength, Email.TooLong.Message)
            .RegexFormat<CreateRegUserCommand, string>(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", Email.InvalidFormat.Message);

        RuleFor(x => x.UserName)
            .NotEmpty<CreateRegUserCommand, string, string>(ValidationErrors.RegUser.UserNameTooLong.Message)
            .MaximumLength<CreateRegUserCommand, string>(Domain.Model.Domain.RegUser.UsernameMaxLength,
                ValidationErrors.RegUser.UserNameTooLong.Message);

        RuleFor(x => x.Password)
            .NotEmpty<CreateRegUserCommand, string, string>(ValidationErrors.RegUser.PasswordTooLong.Message)
            .MaximumLength<CreateRegUserCommand, string>(Domain.Model.Domain.RegUser.PasswordMaxLength,
                ValidationErrors.RegUser.PasswordTooLong.Message);

        RuleFor(x => x.Email).MustAsync((email, cancellationToken) => regUserDataStore.IsEmailUniqueAsync(email, cancellationToken))
        .WithMessage(x => ValidationErrors.RegUser.EmailAlreadyInUse(x.Email).Message);

        RuleFor(x => x.UserName).MustAsync((userName, cancellationToken) => regUserDataStore.IsUsernamelUniqueAsync(userName, cancellationToken))
            .WithMessage(x => ValidationErrors.RegUser.UsernameAlreadyInUse(x.UserName).Message);
    }
}