using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class RemoveRegUserCommandHandler : ICommandHandler<RemoveRegUserCommand, string>
{
    private readonly ILogger<RemoveRegUserCommandHandler> _logger;
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveRegUserCommandHandler(ILogger<RemoveRegUserCommandHandler> logger,
        IRegUserDataStore regUserDataStore,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _regUserDataStore = regUserDataStore;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(RemoveRegUserCommand request, CancellationToken cancellationToken)
    {
        var regUser = await _regUserDataStore.GetByGuidId(request.RegUserId, cancellationToken);

        if (regUser is null)
        {
            return Result.Failure<string>(ValidationErrors.RegUser.NotFound(request.RegUserId));
        }

        _regUserDataStore.Remove(regUser);

        await _unitOfWork.SaveChangesAsync(request.AppUser, cancellationToken);

        return regUser.FullName;
    }
}
