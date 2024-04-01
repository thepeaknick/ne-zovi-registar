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

namespace NeZoviReg.Application.Services.UserAccount;

public class ModifyRegUserAccountsCommandValidator : AbstractValidator<ModifyRegUserAccountsCommand>
{
    
    public ModifyRegUserAccountsCommandValidator(IUserAccountDataStore userAccountDataStore)
    {
        RuleForEach(x => x.Accounts).ChildRules(ua =>
        {
            ua.RuleFor(x => x.Username)
                .NotEmpty().WithMessage(UserName.Empty.Message)
                .MaximumLength(Domain.Model.Auth.UserAccount.UsernameMaxLength).WithMessage(UserName.TooLong.Message);
            
            ua.RuleFor(x => x.Password)
                .NotEmpty().WithMessage(Password.Empty.Message)
                .MaximumLength(Domain.Model.Auth.UserAccount.PasswordMaxLength).WithMessage(Password.TooLong.Message);
            
            ua.RuleFor(x => x.Username).MustAsync(async (userName, cancellationToken) =>
                    !(await userAccountDataStore.IsUsernameExistsAsync(userName, cancellationToken: cancellationToken)))
                .WithMessage(x => UserName.AlreadyInUse(x.Username).Message);
            
        });
       
    }
}