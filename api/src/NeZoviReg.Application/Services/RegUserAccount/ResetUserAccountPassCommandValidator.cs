using FluentValidation;
using NeZoviReg.Abstractions.Extensions;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.RegUserAccount;

public class ResetUserAccountPassCommandValidator : AbstractValidator<ResetPassCommand>
{
    public ResetUserAccountPassCommandValidator()
    {
        RuleFor(x => x.Email)!
            .NotEmpty<ResetPassCommand, string, bool>(RegErrors.Email.Empty.Message)
            .RegexFormat<ResetPassCommand, bool>(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegErrors.Email.InvalidFormat.Message);

        RuleFor(x => x.Token)
            .NotEmpty<ResetPassCommand, string, bool>(Token.ForgotPasswordTokenEmpty.Message);
        
        RuleFor(x => x.Password)
            .NotEmpty<ResetPassCommand, string, bool>(Password.Empty.Message);
    }
}