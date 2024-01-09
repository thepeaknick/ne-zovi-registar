using FluentValidation;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.RegUser;

public class CreateRegUserCommandValidator : AbstractValidator<CreateRegUserCommand>
{
    public CreateRegUserCommandValidator(IRegUserDataStore regUserDataStore)
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty<CreateRegUserCommand, string, RegUserDto>(CompanyName.Empty.Message)
            .MaximumLength<CreateRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.CompanyNameMaxLength, CompanyName.TooLong.Message);

        RuleFor(x => x.Email)!
            .NotEmpty<CreateRegUserCommand, string, RegUserDto>(RegErrors.Email.Empty.Message)
            .RegexFormat<CreateRegUserCommand, RegUserDto>(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegErrors.Email.InvalidFormat.Message);
        
        RuleFor(x => x.FirstName)
            .NotEmpty<CreateRegUserCommand, string, RegUserDto>(FirstName.Empty.Message)
            .MaximumLength<CreateRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.FirstNameMaxLength, FirstName.TooLong.Message);

        RuleFor(x => x.LastName)
            .NotEmpty<CreateRegUserCommand, string, RegUserDto>(LastName.Empty.Message)
            .MaximumLength<CreateRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.LastNameMaxLength, LastName.TooLong.Message);

        RuleFor(x => x.Address)
            .NotEmpty<CreateRegUserCommand, string, RegUserDto>(Address.Empty.Message)
            .MaximumLength<CreateRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.AddressMaxLength, Address.TooLong.Message);

        RuleFor(x => x.RegNumber)
            .NotEmpty<CreateRegUserCommand, string, RegUserDto>(RegNumber.Empty.Message)
            .MaximumLength<CreateRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.RegNumberMaxLength, RegNumber.TooLong.Message);

        RuleFor(x => x.TaxNumber)
            .NotEmpty<CreateRegUserCommand, string, RegUserDto>(TaxNumber.Empty.Message)
            .MaximumLength<CreateRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.TaxNumberMaxLength, TaxNumber.TooLong.Message);

        RuleFor(x => x.UserName)
            .NotEmpty<CreateRegUserCommand, string, RegUserDto>(UserName.Empty.Message)
            .MaximumLength<CreateRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.UsernameMaxLength,
                UserName.TooLong.Message);

        RuleFor(x => x.Password)
            .NotEmpty<CreateRegUserCommand, string, RegUserDto>(Password.Empty.Message)
            .MaximumLength<CreateRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.PasswordMaxLength,
                Password.TooLong.Message);
        
        RuleFor(x => x.Email).MustAsync(async (mail, cancellationToken) =>
                !(await regUserDataStore.IsEmailExistsAsync(mail, cancellationToken: cancellationToken)))
            .WithMessage(x => RegErrors.Email.AlreadyInUse(x.Email).Message);
        
        RuleFor(x => x.CompanyName).MustAsync(async (name, cancellationToken) =>
                !(await regUserDataStore.IsCompanyNameExistsAsync(name, cancellationToken: cancellationToken)))
            .WithMessage(x => CompanyName.AlreadyInUse(x.CompanyName).Message);

        RuleFor(x => x.RegNumber).MustAsync(async (regNumb, cancellationToken) =>
                !(await regUserDataStore.IsRegNumberExistsAsync(regNumb, cancellationToken: cancellationToken)))
            .WithMessage(x => RegNumber.AlreadyInUse(x.RegNumber).Message);

        RuleFor(x => x.TaxNumber).MustAsync(async (taxNumb, cancellationToken) =>
                !(await regUserDataStore.IsTaxNumberExistsAsync(taxNumb, cancellationToken: cancellationToken)))
            .WithMessage(x => TaxNumber.AlreadyInUse(x.TaxNumber).Message);

        RuleFor(x => x.UserName).MustAsync(async (userName, cancellationToken) =>
                !(await regUserDataStore.IsUsernameExistsAsync(userName, cancellationToken: cancellationToken)))
            .WithMessage(x => UserName.AlreadyInUse(x.UserName).Message);
    }
}