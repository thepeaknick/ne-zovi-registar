using FluentValidation;

namespace NeZoviReg.Auth.Services.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().MaximumLength(Domain.Model.Domain.RegUser.EmailMaxLength);
    }
}