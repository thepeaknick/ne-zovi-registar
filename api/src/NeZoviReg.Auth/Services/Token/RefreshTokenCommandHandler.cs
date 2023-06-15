using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
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
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUnitOfWork _unitOfWork;
    
    public RefreshTokenCommandHandler(
        IRegUserDataStore regUserDataStore,
        IJwtProvider jwtProvider,
        IUnitOfWork unitOfWork)
    {
        _regUserDataStore = regUserDataStore;
        _jwtProvider = jwtProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RefreshTokenResultDto>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var refrehTokenResult = await _jwtProvider.RefreshTokenAsync(command.AccessToken, command.RefreshToken, cancellationToken);

        var regUser = refrehTokenResult.RegUser;

        if (regUser is null)
        {
            Log.Information($"RegUser with AccessToken={command.AccessToken};RefreshToken={command.RefreshToken} does not exist.");
            
            return Result.Failure<RefreshTokenResultDto>(RegErrors.RegUser.NotRegistered);
        }

        regUser.WithRefreshToken(refrehTokenResult.RefreshToken.TokenString)
            .WithRefreshTokenExpTime(refrehTokenResult.RefreshToken.ExpireAt);

        _regUserDataStore.Update(regUser);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);

        Log.Information("RegUser refresh access and refresh token.");
        
        return new RefreshTokenResultDto(refrehTokenResult.AccessToken, refrehTokenResult.AccessTokenExpTime,
            refrehTokenResult.RefreshToken.TokenString, refrehTokenResult.RefreshToken.ExpireAt);
    }
}
