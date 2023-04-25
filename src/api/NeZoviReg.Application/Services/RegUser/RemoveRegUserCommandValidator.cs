using FluentValidation;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Messaging.Domain.Commands;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.RegUser;

public class RemoveRegUserCommandValidator : AbstractValidator<RemoveRegUserCommand>
{
    public RemoveRegUserCommandValidator()
    {
        RuleFor(x => x.RegUserId)
            .NotEmpty<RemoveRegUserCommand, Guid, string>(ValidationErrors.RegUser.IdentificatorEmpty.Message);
    }
}