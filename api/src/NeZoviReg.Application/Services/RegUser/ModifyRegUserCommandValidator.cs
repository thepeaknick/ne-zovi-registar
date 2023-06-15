using FluentValidation;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;

namespace NeZoviReg.Application.Services.RegUser;

public class ModifyRegUserCommandValidator : AbstractValidator<ModifyRegUserCommand>
{
    public ModifyRegUserCommandValidator(IRegUserDataStore regUserDataStore)
    {
        RuleFor(x => x.RegUserId)
            .NotEmpty<ModifyRegUserCommand, Guid, RegUserDto>(RegErrors.RegUser.IdentificatorEmpty.Message);

        When(x => !string.IsNullOrEmpty(x.CompanyName), () =>
        {
            RuleFor(x => x.CompanyName)!
                .MaximumLength<ModifyRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.CompanyNameMaxLength,
                    CompanyName.TooLong.Message);

            RuleFor(x => x.CompanyName).CustomAsync(async (name, ctx, cancellationToken) =>
            {
                if (!await regUserDataStore.IsCompanyNameUniqueAsync(name!, ctx.InstanceToValidate.RegUserId,
                        cancellationToken))
                {
                    ctx.AddFailure(CompanyName.AlreadyInUse(name!).Message);
                }
            });
        });
        
        When(x => !string.IsNullOrEmpty(x.Email), () =>
        {
            RuleFor(x => x.Email)!
                .MaximumLength<ModifyRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.EmailMaxLength, RegErrors.Email.TooLong.Message)
                .RegexFormat<ModifyRegUserCommand, RegUserDto>(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegErrors.Email.InvalidFormat.Message);
            
            RuleFor(x => x.Email).CustomAsync(async (mail, ctx, cancellationToken) =>
            {
                if (!await regUserDataStore.IsEmailUniqueAsync(mail!, ctx.InstanceToValidate.RegUserId,
                        cancellationToken))
                {
                    ctx.AddFailure(RegErrors.Email.AlreadyInUse(mail!).Message);
                }
            });
        });

        When(x => !string.IsNullOrEmpty(x.Address), () =>
        {
            RuleFor(x => x.Address)!
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
                if (!await regUserDataStore.IsRegNumberUniqueAsync(regNumber!, ctx.InstanceToValidate.RegUserId,
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
                if (!await regUserDataStore.IsTaxNumberUniqueAsync(taxNumber!, ctx.InstanceToValidate.RegUserId,
                        cancellationToken))
                {
                    ctx.AddFailure(TaxNumber.AlreadyInUse(taxNumber!).Message);
                }
            });
        });

        When(x => !string.IsNullOrEmpty(x.FirstName), () =>
        {
            RuleFor(x => x.FirstName)!
                .MaximumLength<ModifyRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.FirstNameMaxLength,
                    FirstName.TooLong.Message);
        });

        When(x => !string.IsNullOrEmpty(x.LastName), () =>
        {
            RuleFor(x => x.LastName)!
                .MaximumLength<ModifyRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.LastNameMaxLength,
                    LastName.TooLong.Message);
        });

        When(x => !string.IsNullOrEmpty(x.UserName), () =>
        {
            RuleFor(x => x.UserName!)
                .MaximumLength<ModifyRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.UsernameMaxLength, UserName.TooLong.Message);
        });
    }
}