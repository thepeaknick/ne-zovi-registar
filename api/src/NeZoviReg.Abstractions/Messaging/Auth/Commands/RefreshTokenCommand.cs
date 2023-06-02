using NeZoviReg.Abstractions.Messaging.Auth.Model;

namespace NeZoviReg.Abstractions.Messaging.Auth.Commands;

public record RefreshTokenCommand(string AccessToken, string RefreshToken)
    : BaseCommand<RefreshTokenResultDto>;
    