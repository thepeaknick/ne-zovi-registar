using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.User;
using NeZoviReg.Abstractions.Messaging.Domain.Model;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.User;

internal sealed class RemoveUserCommandHandler : ICommandHandler<RemoveUserCommand, UserDto>
{
    private readonly ILogger<RemoveUserCommandHandler> _logger;
    private readonly IUserDataStore _userDataStore;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveUserCommandHandler(ILogger<RemoveUserCommandHandler> logger, IUserDataStore userDataStore, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _userDataStore = userDataStore;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDto>> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userDataStore.GetByPhoneNumber(request.PhoneNumber, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserDto>(RegErrors.User.NotFound(request.PhoneNumber));
        }

        _userDataStore.Remove(user);

        await _unitOfWork.SaveChangesAsync(request.AppUser, cancellationToken);

        return new UserDto(user.PhoneNumber, DateTime.Now);
    }
}
