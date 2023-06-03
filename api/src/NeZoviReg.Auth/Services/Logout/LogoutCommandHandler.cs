using MediatR;
using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Auth.Commands;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Events;

namespace NeZoviReg.Auth.Services.Logout;

internal sealed class LogoutCommandHandler : ICommandHandler<LogoutCommand, bool>
{
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;
    private readonly ILogger<LogoutCommandHandler> _logger;

    public LogoutCommandHandler(
        IRegUserDataStore regUserDataStore,
        IUnitOfWork unitOfWork,
        IPublisher publisher,
        ILogger<LogoutCommandHandler> logger)
    {
        _regUserDataStore = regUserDataStore;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _publisher = publisher;
    }

    public async Task<Result<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var regUser = await _regUserDataStore.GetByGuidId(request.GuidId, cancellationToken);

        if (regUser is null)
        {
            return Result.Failure<bool>(RegErrors.RegUser.Unknown);
        }

        regUser.WithoutRefreshToken();

        _regUserDataStore.Update(regUser);

        await _unitOfWork.SaveChangesAsync(request.AppUser, cancellationToken);

        await _publisher.Publish(new RegUserModifiedEvent
        {
            RegUserId = regUser.GuidId
        }, cancellationToken);

        return true;
    }
}
