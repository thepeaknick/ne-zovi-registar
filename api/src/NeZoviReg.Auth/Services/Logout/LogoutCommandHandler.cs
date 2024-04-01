using MediatR;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Auth.Commands;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Events;
using Serilog;

namespace NeZoviReg.Auth.Services.Logout;

internal sealed class LogoutCommandHandler : ICommandHandler<LogoutCommand, bool>
{
    private readonly IUserAccountDataStore _userAccountDataStore;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public LogoutCommandHandler(
        IUserAccountDataStore userAccountDataStore,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _userAccountDataStore = userAccountDataStore;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result<bool>> Handle(LogoutCommand command, CancellationToken cancellationToken)
    {
        var userAccount = await _userAccountDataStore.GetByGuidId(command.GuidId, cancellationToken);

        if (userAccount is null)
        {
            Log.Information($"UserAccount with GuidId={command.GuidId} does not exist.");
            
            return Result.Failure<bool>(RegErrors.RegUserAccount.Unknown);
        }

        userAccount.WithoutAccessTokenExpTime()
            .WithoutRefreshToken();

        _userAccountDataStore.Update(userAccount);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);

        await _publisher.Publish(new RegUserModifiedEvent
        {
            RegUserId = userAccount.RegUser.GuidId
        }, cancellationToken);

        Log.Information($"UserAccount with GuidId={command.GuidId} logged out.");
        
        return true;
    }
}
