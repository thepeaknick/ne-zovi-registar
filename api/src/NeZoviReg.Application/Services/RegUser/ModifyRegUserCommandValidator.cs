using FluentValidation;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Infrastructure.WebClient;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;

namespace NeZoviReg.Application.Services.RegUser;

public class ModifyRegUserCommandValidator : AbstractValidator<ModifyRegUserCommand>
{
    public ModifyRegUserCommandValidator(IRegUserDataStore regUserDataStore, IAprWebClient aprWebClient)
    {
        RuleFor(x => x.RegUserId)
            .NotEmpty<ModifyRegUserCommand, Guid, RegUserDto>(RegErrors.RegUser.IdentificatorEmpty.Message)
            .DependentRules(() =>
            {
                RuleFor(x => x).CustomAsync(async (x, ctx, cancellationToken) =>
                {
                    var regUser = await regUserDataStore.GetByGuidId(x.RegUserId, cancellationToken);
                    if (regUser is not null)
                    {
                        var regUserApr = await aprWebClient.GetAprBusinessEntityAsync(regUser.RegNumber, cancellationToken);

                        if (regUserApr is null || (!string.IsNullOrEmpty(ctx.InstanceToValidate.RegNumber) && regUserApr.RegNumber != ctx.InstanceToValidate.RegNumber)
                                               || (!string.IsNullOrEmpty(ctx.InstanceToValidate.CompanyName) && regUserApr.CompanyName != ctx.InstanceToValidate.CompanyName)
                                               || (!string.IsNullOrEmpty(ctx.InstanceToValidate.TaxNumber) && regUserApr.TaxNumber != ctx.InstanceToValidate.TaxNumber)
                                               || (!string.IsNullOrEmpty(ctx.InstanceToValidate.Email) && !string.IsNullOrEmpty(regUserApr.Email) && regUserApr.Email != ctx.InstanceToValidate.Email))
                        {
                            ctx.AddFailure(RegErrors.RegUser.InvalidData.Message);
                        }
                    }

                });
            });
        
        When(x => !string.IsNullOrEmpty(x.CompanyName), () =>
        {
            RuleFor(x => x.CompanyName)!
                .MaximumLength<ModifyRegUserCommand, RegUserDto>(Domain.Model.Domain.RegUser.CompanyNameMaxLength,
                    CompanyName.TooLong.Message);

            RuleFor(x => x.CompanyName).CustomAsync(async (name, ctx, cancellationToken) =>
            {
                if (await regUserDataStore.IsCompanyNameExistsAsync(name!, ctx.InstanceToValidate.RegUserId,
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
                if (await regUserDataStore.IsEmailExistsAsync(mail!, ctx.InstanceToValidate.RegUserId,
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
                if (await regUserDataStore.IsRegNumberExistsAsync(regNumber!, ctx.InstanceToValidate.RegUserId,
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
                if (await regUserDataStore.IsTaxNumberExistsAsync(taxNumber!, ctx.InstanceToValidate.RegUserId,
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
            
            RuleFor(x => x.UserName).CustomAsync(async (userName, ctx, cancellationToken) =>
            {
                if (await regUserDataStore.IsUsernameExistsAsync(userName!, ctx.InstanceToValidate.RegUserId,
                        cancellationToken))
                {
                    ctx.AddFailure(UserName.AlreadyInUse(userName!).Message);
                }
            });
        });
    }
}