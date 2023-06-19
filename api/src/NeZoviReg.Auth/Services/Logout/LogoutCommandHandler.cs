using MediatR;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
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
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public LogoutCommandHandler(
        IRegUserDataStore regUserDataStore,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _regUserDataStore = regUserDataStore;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result<bool>> Handle(LogoutCommand command, CancellationToken cancellationToken)
    {
        var regUser = await _regUserDataStore.GetByGuidId(command.GuidId, cancellationToken);

        if (regUser is null)
        {
            Log.Information($"RegUser with RegUserId={command.GuidId} does not exist.");
            
            return Result.Failure<bool>(RegErrors.RegUser.Unknown);
        }

        regUser.WithoutRefreshToken();

        _regUserDataStore.Update(regUser);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);

        await _publisher.Publish(new RegUserModifiedEvent
        {
            RegUserId = regUser.GuidId
        }, cancellationToken);

        Log.Information($"RegUser with RegUserId={command.GuidId} logged out.");
        
        return true;
    }
}
