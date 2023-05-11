using FluentValidation;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model;

namespace NeZoviReg.Application.Services.RegUser;

public class ModifyRegUserCommandValidator : AbstractValidator<ModifyRegUserCommand>
{
    public ModifyRegUserCommandValidator(IRegUserDataStore regUserDataStore)
    {
        RuleFor(x => x.RegUserId)
            .NotEmpty<ModifyRegUserCommand, Guid, RegUserDto>(RegErrors.RegUser.IdentificatorEmpty.Message);

        When(x => !string.IsNullOrEmpty(x.Name), () =>
        {
            RuleFor(x => x.Name)!
                .MaximumLength<ModifyRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.NameMaxLength,
                    Name.TooLong.Message);
        });

        When(x => !string.IsNullOrEmpty(x.Address), () =>
        {
            RuleFor(x => x.Name)!
                .MaximumLength<ModifyRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.AddressMaxLength,
                    Address.TooLong.Message);
        });

        When(x => !string.IsNullOrEmpty(x.RegNumber), () =>
        {
            RuleFor(x => x.RegNumber)!
                .MaximumLength<ModifyRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.RegNumberMaxLength,
                    RegNumber.TooLong.Message);

            RuleFor(x => x.RegNumber).CustomAsync(async (regNumber, ctx, cancellationToken) =>
            {
                if (await regUserDataStore.IsRegNumberUniqueAsync(regNumber!, ctx.InstanceToValidate.RegUserId,
                        cancellationToken))
                {
                    ctx.AddFailure(RegNumber.AlreadyInUse(regNumber!).Message);
                }
            });
        });

        When(x => !string.IsNullOrEmpty(x.TaxNumber), () =>
        {
            RuleFor(x => x.TaxNumber)!
                .MaximumLength<ModifyRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.TaxNumberMaxLength,
                    TaxNumber.TooLong.Message);

            RuleFor(x => x.TaxNumber).CustomAsync(async (taxNumber, ctx, cancellationToken) =>
            {
                if (await regUserDataStore.IsTaxNumberUniqueAsync(taxNumber!, ctx.InstanceToValidate.RegUserId,
                        cancellationToken))
                {
                    ctx.AddFailure(TaxNumber.AlreadyInUse(taxNumber!).Message);
                }
            });
        });


        When(x => !string.IsNullOrEmpty(x.UserName), () =>
        {
            RuleFor(x => x.UserName!)
                .MaximumLength<ModifyRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.UsernameMaxLength, UserName.TooLong.Message);
        });

        When(x => !string.IsNullOrEmpty(x.Password), () =>
        {
            RuleFor(x => x.Password!)
                .MaximumLength<ModifyRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.PasswordMaxLength,
                    Password.TooLong.Message);
        });
    }
}