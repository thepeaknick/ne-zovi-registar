using FluentValidation;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Infrastructure.WebClient;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model.UserAccount;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.RegUserAccount;

public class ModifyRegUserAccountsCommandValidator : AbstractValidator<ModifyRegUserAccountsCommand>
{
    public ModifyRegUserAccountsCommandValidator()
    {
        RuleForEach(x => x.Accounts).ChildRules(ua =>
        {
            ua.RuleFor(x => x.Username)
                .NotEmpty().WithMessage(UserName.Empty.Message)
                .MaximumLength(Domain.Model.Auth.RegUserAccount.UsernameMaxLength)
                .WithMessage(UserName.TooLong.Message);

            ua.RuleFor(x => x.Password)
                .NotEmpty().WithMessage(Password.Empty.Message)
                .MaximumLength(Domain.Model.Auth.RegUserAccount.PasswordMaxLength)
                .WithMessage(Password.TooLong.Message);
        });
    }
}