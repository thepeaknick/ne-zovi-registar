using FluentValidation;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.User;
using NeZoviReg.Abstractions.Messaging.Domain.Model;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;

namespace NeZoviReg.Application.Services.User;

public class ModifyUserCommandValidator : AbstractValidator<ModifyUserCommand>
{
    public ModifyUserCommandValidator(IUserDataStore userDataStore, IRegUserDataStore regUserDataStore)
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty<ModifyUserCommand, string, UserDto>(PhoneNumber.Empty.Message);

        When(x => !string.IsNullOrEmpty(x.FirstName), () =>
        {
            RuleFor(x => x.FirstName)!
                .MaximumLength<ModifyUserCommand, UserDto>(Domain.Model.Domain.User.FirstNameMaxLength,
                    FirstName.TooLong.Message);
        });

        When(x => !string.IsNullOrEmpty(x.LastName), () =>
        {
            RuleFor(x => x.LastName)!
                .MaximumLength<ModifyUserCommand, UserDto>(Domain.Model.Domain.User.LastNameMaxLength,
                    LastName.TooLong.Message);
        });

        When(x => !string.IsNullOrEmpty(x.Jmbg), () =>
        {
            RuleFor(x => x.Jmbg)!
                .MaximumLength<ModifyUserCommand, UserDto>(Domain.Model.Domain.User.JmbgMaxLength, Jmbg.TooLong.Message);
        });

        When(x => !string.IsNullOrEmpty(x.NewPhoneNumber), () =>
        {
            RuleFor(x => x.NewPhoneNumber)!
                .MaximumLength<ModifyUserCommand, UserDto>(Domain.Model.Domain.User.PhoneNumberMaxLength, PhoneNumber.TooLong.Message)
                .RegexFormat<ModifyUserCommand, UserDto>(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", PhoneNumber.InvalidFormat.Message)
                .MustAsync((phone, cancellationToken) => userDataStore.IsPhoneNumberUniqueAsync(phone, cancellationToken))
                .WithMessage(x => PhoneNumber.AlreadyInUse(x.NewPhoneNumber!).Message);;
        });

        When(x => x.NewOperatorId.HasValue, () =>
        {
            RuleFor(x => x.NewOperatorId).CustomAsync(async (id, ctx, cancellationToken) =>
            {
                var regUser = await regUserDataStore.GetById(id!.Value, cancellationToken);
                if (regUser is null || regUser.RegUserRoles.All(x => x.RoleId != (int)RoleType.Obveznik))
                {
                    ctx.AddFailure(RegErrors.RegUser.RoleNotFound(RoleType.Obveznik).Message);
                }
            });
        });
    }
}