using FluentValidation;
using NeZoviReg.Abstractions.Extensions;
using NeZoviReg.Abstractions.Messaging.Auth.Commands;
using NeZoviReg.Abstractions.Messaging.Auth.Model;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Auth.Services.Token;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.AccessToken)
            .NotEmpty<RefreshTokenCommand, string, RefreshTokenResultDto>(RegErrors.Token.AccessTokenEmpty);

        RuleFor(x => x.RefreshToken)
            .NotEmpty<RefreshTokenCommand, string, RefreshTokenResultDto>(RegErrors.Token.RefreshTokenEmpty);
    }
}