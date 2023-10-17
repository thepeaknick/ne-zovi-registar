using FluentValidation;
using Microsoft.Extensions.Options;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.User;
using NeZoviReg.Abstractions.Options;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;

namespace NeZoviReg.Application.Services.User;

public class AddUsersCommandValidator : AbstractValidator<AddUsersCommand>
{
    public AddUsersCommandValidator(IUserDataStore userDataStore, IRegUserDataStore regUserDataStore, IOptionsMonitor<BulkOptions> options)
    {
        RuleFor(x => x.Users.Count).LessThan(options.CurrentValue.BatchSize + 1)
            .WithMessage(RegErrors.User.BatchSizeExceeded(options.CurrentValue.BatchSize).Message);
        
        RuleFor(x => x.Users.Select(u => u.PhoneNumber).ToList())
            .NotEmpty().WithMessage(PhoneNumber.EmptyList.Message)
            .Custom((numbers, ctx) =>
            {
                HashSet<string> set = new();
                if (!numbers.All(set.Add))
                {
                    ctx.AddFailure(PhoneNumber.DuplicatesInList.Message);
                }
            });
        
        RuleForEach(x => x.Users).ChildRules(user =>
        {
            user.RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(FirstName.Empty.Message)
                .MaximumLength(Domain.Model.Domain.User.FirstNameMaxLength).WithMessage(FirstName.TooLong.Message);

            user.RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(LastName.Empty.Message)
                .MaximumLength(Domain.Model.Domain.User.LastNameMaxLength).WithMessage(LastName.TooLong.Message);

            user.RuleFor(x => x.Jmbg)
                .NotEmpty().WithMessage(Jmbg.Empty.Message)
                .MaximumLength(Domain.Model.Domain.User.JmbgMaxLength).WithMessage(Jmbg.TooLong.Message);

            user.RuleFor(x => x.OperatorId)
                .NotEmpty().WithMessage(Operater.Empty.Message);

            user.RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage(PhoneNumber.Empty.Message)
                .MaximumLength(Domain.Model.Domain.User.PhoneNumberMaxLength).WithMessage(PhoneNumber.TooLong.Message)
                .Matches(Domain.Model.Domain.User.PhoneNumberRegex)
                .WithMessage(PhoneNumber.InvalidFormat.Message)
                .MustAsync((phone, cancellationToken) =>
                    userDataStore.IsPhoneNumberUniqueAsync(phone, cancellationToken))
                .WithMessage(PhoneNumber.OneOfAlreadyInUse.Message);

            user.RuleFor(x => x.OperatorId).CustomAsync(async (id, ctx, cancellationToken) =>
            {
                var regUser = await regUserDataStore.GetById(id!, cancellationToken);
                if (regUser is null || regUser.RegUserRoles.All(x => x.RoleId != (int) RoleType.Obveznik))
                {
                    ctx.AddFailure(RegErrors.RegUser.RoleNotFound(RoleType.Obveznik).Message);
                }
            });

        });
    }
}