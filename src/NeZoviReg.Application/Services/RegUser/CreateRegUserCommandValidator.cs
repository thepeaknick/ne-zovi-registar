using FluentValidation;

namespace NeZoviReg.Application.Services.RegUser;

public class CreateRegUserCommandValidator : AbstractValidator<CreateRegUserCommand>
{
    public CreateRegUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().MaximumLength(Domain.Model.Domain.RegUser.EmailMaxLength);
        RuleFor(x => x.UserName).NotEmpty().MaximumLength(Domain.Model.Domain.RegUser.UsernameMaxLength);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(Domain.Model.Domain.RegUser.PasswordMaxLength);
    }
}