using FluentValidation;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.User;
using NeZoviReg.Abstractions.Messaging.Domain.Model.User;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;

namespace NeZoviReg.Application.Services.User;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator(IUserDataStore userDataStore, IRegUserDataStore regUserDataStore)
    {
        RuleFor(x => x.FirstName)
            .NotEmpty<AddUserCommand, string, List<UserDto>>(FirstName.Empty.Message)
            .MaximumLength<AddUserCommand, List<UserDto>>(Domain.Model.Domain.User.FirstNameMaxLength,
                FirstName.TooLong.Message);

        RuleFor(x => x.LastName)
            .NotEmpty<AddUserCommand, string, List<UserDto>>(LastName.Empty.Message)
            .MaximumLength<AddUserCommand, List<UserDto>>(Domain.Model.Domain.User.LastNameMaxLength,
                LastName.TooLong.Message);

        RuleFor(x => x.Jmbg)
            .NotEmpty<AddUserCommand, string, List<UserDto>>(Jmbg.Empty.Message)
            .MaximumLength<AddUserCommand, List<UserDto>>(Domain.Model.Domain.User.JmbgMaxLength, Jmbg.TooLong.Message);

        RuleFor(x => x.OperatorId)
            .NotEmpty<AddUserCommand, int, List<UserDto>>(Operater.Empty.Message);

        RuleFor(x => x.PhoneNumbers)
            .NotEmpty<AddUserCommand, string[], List<UserDto>>(PhoneNumber.EmptyList.Message)
            .Custom((numbers,ctx)=>
        {
            HashSet<string> set = new();
            if (!numbers.All(set.Add))
            {
                ctx.AddFailure(PhoneNumber.DuplicatesInList.Message);
            }
        });

        RuleForEach(x => x.PhoneNumbers)
            .NotEmpty<AddUserCommand, string, List<UserDto>>(PhoneNumber.Empty.Message)
            .MaximumLength<AddUserCommand, List<UserDto>>(Domain.Model.Domain.User.PhoneNumberMaxLength, PhoneNumber.TooLong.Message)
            .RegexFormat<AddUserCommand, List<UserDto>>(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", PhoneNumber.InvalidFormat.Message)
            .MustAsync((phone, cancellationToken) => userDataStore.IsPhoneNumberUniqueAsync(phone, cancellationToken))
                .WithMessage(PhoneNumber.OneOfAlreadyInUse.Message);
            
        RuleFor(x => x.OperatorId).CustomAsync(async (id, ctx, cancellationToken) =>
        {
            var regUser = await regUserDataStore.GetById(id!, cancellationToken);
            if (regUser is null || regUser.RegUserRoles.All(x => x.RoleId != (int) RoleType.Obveznik))
            {
                ctx.AddFailure(RegErrors.RegUser.RoleNotFound(RoleType.Obveznik).Message);
            }
        });
    }
}