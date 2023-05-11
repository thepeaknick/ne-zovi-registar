using FluentValidation;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.User;
using NeZoviReg.Abstractions.Shared.Errors;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;

namespace NeZoviReg.Application.Services.User;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator(IUserDataStore userDataStore)
    {
        RuleFor(x => x.FirstName)
            .NotEmpty<AddUserCommand, string, string>(FirstName.Empty.Message)
            .MaximumLength<AddUserCommand, string>(Domain.Model.Domain.User.FirstNameMaxLength,
                FirstName.TooLong.Message);

        RuleFor(x => x.LastName)
            .NotEmpty<AddUserCommand, string, string>(LastName.Empty.Message)
            .MaximumLength<AddUserCommand, string>(Domain.Model.Domain.User.LastNameMaxLength,
                LastName.TooLong.Message);

        RuleFor(x => x.Jmbg)
            .NotEmpty<AddUserCommand, string, string>(Jmbg.Empty.Message)
            .MaximumLength<AddUserCommand, string>(Domain.Model.Domain.User.JmbgMaxLength, Jmbg.TooLong.Message);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty<AddUserCommand, string, string>(PhoneNumber.Empty.Message)
            .MaximumLength<AddUserCommand, string>(Domain.Model.Domain.User.PhoneNumberMaxLength, PhoneNumber.TooLong.Message)
            .RegexFormat<AddUserCommand, string>(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", PhoneNumber.InvalidFormat.Message);

        RuleFor(x => x.PhoneNumber).MustAsync((phone, cancellationToken) => userDataStore.IsPhoneNumberUniqueAsync(phone, cancellationToken))
            .WithMessage(x => RegErrors.User.AlreadyInUse(x.PhoneNumber).Message);
    }
}