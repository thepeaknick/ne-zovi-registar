using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Auth.Commands;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Auth.Authentication.Jwt;

namespace NeZoviReg.Auth.Services.Login;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, string>
{
    private readonly IAuthDataStore _authDataStore;
    private readonly IJwtProvider _jwtProvider;
    private readonly ILogger<LoginCommandHandler> _logger;

    public LoginCommandHandler(
        IAuthDataStore authDataStore,
        IJwtProvider jwtProvider,
        ILogger<LoginCommandHandler> logger)
    {
        _authDataStore = authDataStore;
        _jwtProvider = jwtProvider;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        var regUser = await _authDataStore.GetRegUserByEmailAsync(request.Email, cancellationToken);

        if (regUser is null)
        {
            return Result.Failure<string>(RegErrors.RegUser.NotFound(request.Email));
        }

        var token = await _jwtProvider.GenerateAsync(regUser, cancellationToken);

        return token;
    }
}
