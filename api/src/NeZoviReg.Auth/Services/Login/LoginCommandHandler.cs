using MediatR;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Auth.Commands;
using NeZoviReg.Abstractions.Messaging.Auth.Model;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Events;
using NeZoviReg.Auth.Authentication.Jwt;
using Serilog;

namespace NeZoviReg.Auth.Services.Login;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResultDto>
{
    private readonly IUserAccountDataStore _userAccountDataStore;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;

    public LoginCommandHandler(
        IUserAccountDataStore userAccountDataStore,
        IJwtProvider jwtProvider,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _userAccountDataStore = userAccountDataStore;
        _jwtProvider = jwtProvider;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result<LoginResultDto>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var userAccount =
            await _userAccountDataStore.GetByUsernameAndPassword(command.UserName, command.Password, cancellationToken);

        if (userAccount is null)
        {
            Log.Information($"RegUser with UserName={command.UserName} does not exist.");

            return Result.Failure<LoginResultDto>(RegErrors.RegUserAccount.InvalidCredentials);
        }

        if (userAccount.IsAccessTokenValid)
        {
            Log.Information($"RegUser with UserName={command.UserName} has an active session. Logging out...");
        }

        var loginResult = await _jwtProvider.GenerateTokenAsync(userAccount, cancellationToken: cancellationToken);

        userAccount.WithAccessTokenExpTime(loginResult.AccessTokenExpTime)
            .WithRefreshToken(loginResult.RefreshToken.TokenString)
            .WithRefreshTokenExpTime(loginResult.RefreshToken.ExpireAt);

        _userAccountDataStore.Update(userAccount);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);

        await _publisher.Publish(new RegUserModifiedEvent
        {
            RegUserId = userAccount.RegUser.GuidId
        }, cancellationToken);

        Log.Information($"RegUser with UserName={command.UserName} logged in.");

        return new LoginResultDto(userAccount.RegUser.GuidId,
            userAccount.GuidId,
            loginResult.AccessToken,
            loginResult.AccessTokenExpTime,
            loginResult.RefreshToken.TokenString,
            loginResult.RefreshToken.ExpireAt);
    }
}