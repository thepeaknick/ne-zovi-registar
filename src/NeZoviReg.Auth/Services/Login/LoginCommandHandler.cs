using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Auth.Authentication.Jwt;
using NeZoviReg.Auth.Infrastructure;
using NeZoviReg.Domain.Errors;
using NeZoviReg.Domain.Model;
using NeZoviReg.Domain.Shared;

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
        var regUser = await _authDataStore.GetRegUserByEmailAsync(request.Email, cancellationToken);

        if (regUser is null)
        {
            return Result.Failure<string>(DomainErrors.RegUser.InvalidCredentials);
        }

        var token = await _jwtProvider.GenerateAsync(regUser, cancellationToken);

        return token;
    }
}
