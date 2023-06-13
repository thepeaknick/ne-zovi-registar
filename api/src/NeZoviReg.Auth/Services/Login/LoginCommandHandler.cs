using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Auth.Commands;
using NeZoviReg.Abstractions.Messaging.Auth.Model;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Auth.Authentication.Jwt;

namespace NeZoviReg.Auth.Services.Login;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResultDto>
{
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LoginCommandHandler> _logger;

    public LoginCommandHandler(
        IRegUserDataStore regUserDataStore,
        IJwtProvider jwtProvider,
        IUnitOfWork unitOfWork,
        ILogger<LoginCommandHandler> logger)
    {
        _regUserDataStore = regUserDataStore;
        _jwtProvider = jwtProvider;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<LoginResultDto>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var regUser = await _regUserDataStore.GetByUsernameAndPassword(command.UserName, command.Password, cancellationToken);

        if (regUser is null)
        {
            _logger.LogInformation($"RegUser with UserName={command.UserName} does not exist.");
            
            return Result.Failure<LoginResultDto>(RegErrors.RegUser.InvalidCredentials);
        }

        var loginResult = await _jwtProvider.GenerateTokenAsync(regUser, cancellationToken);

        regUser.WithRefreshToken(loginResult.RefreshToken.TokenString)
            .WithRefreshTokenExpTime(loginResult.RefreshToken.ExpireAt);
        _regUserDataStore.Update(regUser);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);

        return new LoginResultDto(regUser.GuidId, 
            loginResult.AccessToken, 
            loginResult.AccessTokenExpTime, 
            loginResult.RefreshToken.TokenString,
            loginResult.RefreshToken.ExpireAt);
    }
}
