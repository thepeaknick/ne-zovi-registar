using FluentValidation;
using NeZoviReg.Abstractions.Extensions;
using static NeZoviReg.Abstractions.Shared.Errors.ValidationErrors;

namespace NeZoviReg.Application.Services.User;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
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
            .RegexFormat<AddUserCommand, string>(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", Email.InvalidFormat.Message);
    }
}