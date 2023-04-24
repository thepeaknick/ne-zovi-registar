using FluentValidation;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Application.Infrastructure.DataStores;

namespace NeZoviReg.Application.Services.RegUser;

public class CreateRegUserCommandValidator : AbstractValidator<CreateRegUserCommand>
{
    private readonly IRegUserDataStore _regUserDataStore;

    public CreateRegUserCommandValidator(IRegUserDataStore regUserDataStore)
    {
        _regUserDataStore = regUserDataStore;

        RuleFor(x => x.Email).NotEmpty().MaximumLength(Domain.Model.Domain.RegUser.EmailMaxLength);
        RuleFor(x => x.UserName).NotEmpty().MaximumLength(Domain.Model.Domain.RegUser.UsernameMaxLength);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(Domain.Model.Domain.RegUser.PasswordMaxLength);

        RuleFor(x => x.Email).CustomAsync(async (email, ctx, cancellationToken) =>
        {
            if (!await _regUserDataStore.IsEmailUniqueAsync(email, cancellationToken))
            {
                ctx.AddFailure(ValidationErrors.RegUser.EmailAlreadyInUse(email).Message);
            }
        });
    }
}