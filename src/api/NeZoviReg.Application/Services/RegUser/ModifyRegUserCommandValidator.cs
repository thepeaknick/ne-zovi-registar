using FluentValidation;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging.Domain.Commands;
using static NeZoviReg.Abstractions.Shared.Errors.ValidationErrors;

namespace NeZoviReg.Application.Services.RegUser;

public class ModifyRegUserCommandValidator : AbstractValidator<ModifyRegUserCommand>
{
    public ModifyRegUserCommandValidator(IRegUserDataStore regUserDataStore)
    {
        RuleFor(x => x.RegUserId)
            .NotEmpty<ModifyRegUserCommand, Guid, string>(ValidationErrors.RegUser.IdentificatorEmpty.Message);

        When(x => !string.IsNullOrEmpty(x.Email), () =>
        {
            RuleFor(x => x.Email!)
                .MaximumLength<ModifyRegUserCommand, string>(Domain.Model.Domain.RegUser.EmailMaxLength, Email.TooLong.Message)
                .RegexFormat<ModifyRegUserCommand, string>(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", Email.InvalidFormat.Message);
        });

        When(x => !string.IsNullOrEmpty(x.UserName), () =>
        {
            RuleFor(x => x.UserName!)
                .MaximumLength<ModifyRegUserCommand, string>(Domain.Model.Domain.RegUser.UsernameMaxLength, ValidationErrors.RegUser.UserNameTooLong.Message);
        });

        When(x => !string.IsNullOrEmpty(x.Password), () =>
        {
            RuleFor(x => x.Password!)
                .MaximumLength<ModifyRegUserCommand, string>(Domain.Model.Domain.RegUser.PasswordMaxLength,
                    ValidationErrors.RegUser.PasswordTooLong.Message);
        });
    }
}