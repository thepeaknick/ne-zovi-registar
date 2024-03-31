using FluentValidation;
using static NeZoviReg.Abstractions.Shared.Errors.RegErrors;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Messaging.Auth.Commands;

namespace NeZoviReg.Auth.Services.Logout;

public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(x => x.GuidId)
            .NotEmpty<LogoutCommand, Guid, bool>(UserAccount.NotLoggedIn.Message);
    }
}