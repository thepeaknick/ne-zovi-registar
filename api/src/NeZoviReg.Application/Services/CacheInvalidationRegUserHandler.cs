using MediatR;
using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Shared.Caching;
using NeZoviReg.Abstractions.Shared.Events;

namespace NeZoviReg.Application.Services;

internal class CacheInvalidationRegUserHandler :
    INotificationHandler<RegUserModifiedEvent>,
    INotificationHandler<RegUserDeletedEvent>
{
    private readonly ICacheService _cacheService;
    private readonly ILogger<CacheInvalidationRegUserHandler> _logger;
    
    public CacheInvalidationRegUserHandler(ICacheService cacheService, ILogger<CacheInvalidationRegUserHandler> logger)
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task Handle(RegUserModifiedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"RegUserModifiedEvent RegUserId={notification.RegUserId} published.");
        
        await HandleInternal(notification.RegUserId, cancellationToken);
    }
    public async Task Handle(RegUserDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"RegUserDeletedEvent RegUserId={notification.RegUserId} published.");
        
        await HandleInternal(notification.RegUserId, cancellationToken);
    }

    private async Task HandleInternal(Guid regUserId, CancellationToken cancellationToken)
    {
        await _cacheService.RemoveAsync($"{CacheKeyPrefix.RegUser}{regUserId}", cancellationToken);
    }
}
