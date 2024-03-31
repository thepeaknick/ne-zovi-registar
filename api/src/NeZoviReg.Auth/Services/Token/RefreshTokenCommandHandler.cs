using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Auth.Commands;
using NeZoviReg.Abstractions.Messaging.Auth.Model;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Auth.Authentication.Jwt;
using Serilog;

namespace NeZoviReg.Auth.Services.Token;

internal sealed class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, RefreshTokenResultDto>
{
    private readonly IUserAccountDataStore _userAccountDataStore;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUnitOfWork _unitOfWork;
    
    public RefreshTokenCommandHandler(
        IUserAccountDataStore userAccountDataStore,
        IJwtProvider jwtProvider,
        IUnitOfWork unitOfWork)
    {
        _userAccountDataStore = userAccountDataStore;
        _jwtProvider = jwtProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RefreshTokenResultDto>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var refrehTokenResult = await _jwtProvider.RefreshTokenAsync(command.AccessToken, command.RefreshToken, cancellationToken);

        var userAccount = refrehTokenResult.UserAccount;

        if (userAccount is null)
        {
            Log.Information($"UserAccount with AccessToken={command.AccessToken};RefreshToken={command.RefreshToken} does not exist.");
            
            return Result.Failure<RefreshTokenResultDto>(RegErrors.RegUser.NotRegistered);
        }

        userAccount.WithRefreshToken(refrehTokenResult.RefreshToken.TokenString)
            .WithRefreshTokenExpTime(refrehTokenResult.RefreshToken.ExpireAt);

        _userAccountDataStore.Update(userAccount);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);

        Log.Information("UserAccount refreshed access and refresh token successfully.");
        
        return new RefreshTokenResultDto(refrehTokenResult.AccessToken, refrehTokenResult.AccessTokenExpTime,
            refrehTokenResult.RefreshToken.TokenString, refrehTokenResult.RefreshToken.ExpireAt);
    }
}
