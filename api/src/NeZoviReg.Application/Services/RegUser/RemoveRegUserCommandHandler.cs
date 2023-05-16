using MediatR;
using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Events;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class RemoveRegUserCommandHandler : ICommandHandler<RemoveRegUserCommand, RegUserDto>
{
    private readonly ILogger<RemoveRegUserCommandHandler> _logger;
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveRegUserCommandHandler(ILogger<RemoveRegUserCommandHandler> logger,
        IRegUserDataStore regUserDataStore,
        IUnitOfWork unitOfWork, IPublisher publisher)
    {
        _logger = logger;
        _regUserDataStore = regUserDataStore;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result<RegUserDto>> Handle(RemoveRegUserCommand request, CancellationToken cancellationToken)
    {
        var regUser = await _regUserDataStore.GetByGuidId(request.RegUserId, cancellationToken);

        if (regUser is null)
        {
            return Result.Failure<RegUserDto>(RegErrors.RegUser.NotFound(request.RegUserId));
        }

        _regUserDataStore.Remove(regUser);

        await _unitOfWork.SaveChangesAsync(request.AppUser, cancellationToken);

        await _publisher.Publish(new RegUserDeletedEvent
        {
            RegUserId = regUser.GuidId
        }, cancellationToken);

        return new RegUserDto(regUser.GuidId, regUser.FullName);
    }
}
