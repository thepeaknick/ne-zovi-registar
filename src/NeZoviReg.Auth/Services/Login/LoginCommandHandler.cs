using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Auth.Authentication.Jwt;
using NeZoviReg.Auth.Infrastructure;
using NeZoviReg.Domain.Model.Auth;

namespace NeZoviReg.Auth.Services.Login;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, string>
{
    private readonly IAuthDataStore _authDataStore;
    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(
        IAuthDataStore authDataStore,
        IJwtProvider jwtProvider)
    {
        _authDataStore = authDataStore;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var t = Role.GetValues();

        var regUser = await _authDataStore.GetRegUserByEmailAsync(request.Email, cancellationToken);

        if (regUser is null)
        {
            return Result.Failure<string>(ValidationErrors.RegUser.InvalidCredentials);
        }

        var token = await _jwtProvider.GenerateAsync(regUser, cancellationToken);

        return token;
    }
}
