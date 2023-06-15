using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.User;
using NeZoviReg.Abstractions.Messaging.Domain.Model.User;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using Serilog;

namespace NeZoviReg.Application.Services.User;

internal sealed class RemoveUserCommandHandler : ICommandHandler<RemoveUserCommand, UserDto>
{
    private readonly IUserDataStore _userDataStore;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveUserCommandHandler(IUserDataStore userDataStore,
        IUnitOfWork unitOfWork)
    {
        _userDataStore = userDataStore;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDto>> Handle(RemoveUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userDataStore.GetByPhoneNumber(command.PhoneNumber, cancellationToken);

        if (user is null)
        {
            Log.Information($"PhoneNumber={command.PhoneNumber} does not exist.");

            return Result.Failure<UserDto>(RegErrors.User.NotFound(command.PhoneNumber));
        }

        _userDataStore.Remove(user);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);

        Log.Information($"PhoneNumber={command.PhoneNumber} removed.");
        
        return new UserDto(user.PhoneNumber, DateTime.Now);
    }
}