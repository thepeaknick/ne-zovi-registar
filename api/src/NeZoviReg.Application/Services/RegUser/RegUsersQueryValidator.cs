using FluentValidation;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.RegUser;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.RegUser;

public class RegUsersQueryValidator : AbstractValidator<RegUsersQuery>
{
    public RegUsersQueryValidator()
    {
        RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage(RegErrors.RegUser.RoleUnknown);
    }
}