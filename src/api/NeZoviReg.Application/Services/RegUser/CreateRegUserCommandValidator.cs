using FluentValidation;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model;

namespace NeZoviReg.Application.Services.RegUser;

public class CreateRegUserCommandValidator : AbstractValidator<CreateRegUserCommand>
{
    public CreateRegUserCommandValidator(IRegUserDataStore regUserDataStore)
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty<CreateRegUserCommand, string, RegUserDto>(CompanyName.Empty.Message)
            .MaximumLength<CreateRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.CompanyNameMaxLength, CompanyName.TooLong.Message);

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
            .NotEmpty<CreateRegUserCommand, string, RegUserDto>(UserName.TooLong.Message)
            .MaximumLength<CreateRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.UsernameMaxLength,
                UserName.TooLong.Message);

        RuleFor(x => x.Password)
            .NotEmpty<CreateRegUserCommand, string, RegUserDto>(Password.TooLong.Message)
            .MaximumLength<CreateRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.PasswordMaxLength,
                Password.TooLong.Message);

        RuleFor(x => x.CompanyName).MustAsync((name, cancellationToken) =>
                regUserDataStore.IsCompanyNameUniqueAsync(name, cancellationToken: cancellationToken))
            .WithMessage(x => CompanyName.AlreadyInUse(x.CompanyName).Message);

        RuleFor(x => x.RegNumber).MustAsync((regNumb, cancellationToken) =>
                regUserDataStore.IsRegNumberUniqueAsync(regNumb, cancellationToken: cancellationToken))
            .WithMessage(x => RegNumber.AlreadyInUse(x.RegNumber).Message);

        RuleFor(x => x.TaxNumber).MustAsync((taxNumb, cancellationToken) =>
                regUserDataStore.IsTaxNumberUniqueAsync(taxNumb, cancellationToken: cancellationToken))
            .WithMessage(x => TaxNumber.AlreadyInUse(x.TaxNumber).Message);

        RuleFor(x => x.UserName).MustAsync((userName, cancellationToken) =>
                regUserDataStore.IsUsernamelUniqueAsync(userName, cancellationToken: cancellationToken))
            .WithMessage(x => UserName.AlreadyInUse(x.UserName).Message);
    }
}