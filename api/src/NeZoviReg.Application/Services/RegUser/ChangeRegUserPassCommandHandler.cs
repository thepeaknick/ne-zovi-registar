using MediatR;
using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Events;
using Serilog;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class ChangeRegUserPassCommandHandler : ICommandHandler<ChangePassCommand, bool>
{
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeRegUserPassCommandHandler(IRegUserDataStore regUserDataStore,
        IPublisher publisher,
        IUnitOfWork unitOfWork)
    {
        _regUserDataStore = regUserDataStore;
        _publisher = publisher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(ChangePassCommand command, CancellationToken cancellationToken)
    {
        var regUser = await _regUserDataStore.GetByUsernameAndPassword(command.UserName, command.Password, cancellationToken);

        if (regUser is null)
        {
            Log.Information($"RegUser with UserName={command.UserName} does not exist.");
            
            return Result.Failure<bool>(RegErrors.RegUser.InvalidCredentials);
        }
        
        regUser.WithoutRefreshToken()
            .WithPassword(command.NewPassword);

        _regUserDataStore.Update(regUser);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);

        await _publisher.Publish(new RegUserModifiedEvent
        {
            RegUserId = regUser.GuidId
        }, cancellationToken);
        
        Log.Information($"RegUser with RegUserId={command.UserName} password changed.");
        
        return true;
    }
}
