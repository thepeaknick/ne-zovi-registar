using MediatR;
using NeZoviReg.Abstractions.Shared.Caching;
using NeZoviReg.Abstractions.Shared.Events;

namespace NeZoviReg.Auth.Services;

internal class CacheInvalidationRegUserHandler :
    INotificationHandler<RegUserModifiedEvent>,
    INotificationHandler<RegUserDeletedEvent>
{
    private readonly ICacheService _cacheService;

    public CacheInvalidationRegUserHandler(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public Task Handle(RegUserModifiedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.RegUserId, cancellationToken);
    }
    public Task Handle(RegUserDeletedEvent notification, CancellationToken cancellationToken)
    {
        return HandleInternal(notification.RegUserId, cancellationToken);
    }

    private async Task HandleInternal(Guid regUserId, CancellationToken cancellationToken)
    {
        await _cacheService.RemoveAsync($"{CacheKeyPrefix.RegUser}{regUserId}", cancellationToken);
    }
}
