using FluentValidation;
using NeZoviReg.Abstractions.Extensions;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.RegUser;

public class ResetRegUserPassCommandValidator : AbstractValidator<ResetPassCommand>
{
    public ResetRegUserPassCommandValidator()
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