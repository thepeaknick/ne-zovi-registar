using MediatR;
using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Events;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class RemoveRegUserCommandHandler : ICommandHandler<RemoveRegUserCommand, bool>
{
    private readonly ILogger<RemoveRegUserCommandHandler> _logger;
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveRegUserCommandHandler(ILogger<RemoveRegUserCommandHandler> logger,
        IRegUserDataStore regUserDataStore,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _logger = logger;
        _regUserDataStore = regUserDataStore;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result<bool>> Handle(RemoveRegUserCommand command, CancellationToken cancellationToken)
    {
        var regUser = await _regUserDataStore.GetByGuidId(command.RegUserId, cancellationToken);

        if (regUser is null)
        {
            _logger.LogInformation($"RegUser with RegUserId={command.RegUserId} does not exist.");
                
            return Result.Failure<bool>(RegErrors.RegUser.NotFound(command.RegUserId));
        }

        _regUserDataStore.Remove(regUser);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);

        await _publisher.Publish(new RegUserDeletedEvent
        {
            RegUserId = regUser.GuidId
        }, cancellationToken);

        return true;
    }
}
