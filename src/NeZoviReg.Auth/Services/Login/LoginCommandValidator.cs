using FluentValidation;
using NeZoviReg.Abstractions.Shared.Enums;
using static NeZoviReg.Abstractions.Shared.Errors.ValidationErrors;

namespace NeZoviReg.Auth.Services.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty()
            .WithErrorCode(ErrorCode.Empty.ToString())
            .WithMessage(RegUser.EmailEmpty.Message)
            .MaximumLength(Domain.Model.Domain.RegUser.EmailMaxLength)
            .WithErrorCode(ErrorCode.TooLong.ToString())
            .WithMessage(RegUser.EmailMaxLength(Domain.Model.Domain.RegUser.EmailMaxLength).Message);
    }
}