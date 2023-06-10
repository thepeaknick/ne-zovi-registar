using FluentValidation;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Messaging.Auth.Commands;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Auth.Services.Login;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPassCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty<ForgotPassCommand, string, string>(RegErrors.Email.Empty.Message);
    }
}