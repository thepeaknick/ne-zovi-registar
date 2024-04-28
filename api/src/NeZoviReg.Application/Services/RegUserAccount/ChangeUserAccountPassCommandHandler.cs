using MediatR;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Events;
using Serilog;

namespace NeZoviReg.Application.Services.RegUserAccount;

internal sealed class ChangeUserAccountPassCommandHandler : ICommandHandler<ChangePassCommand, bool>
{
    private readonly IRegUserAccountDataStore _regUserAccountDataStore;
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeUserAccountPassCommandHandler(IRegUserAccountDataStore regUserAccountDataStore,
        IPublisher publisher,
        IUnitOfWork unitOfWork)
    {
        _regUserAccountDataStore = regUserAccountDataStore;
        _publisher = publisher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(ChangePassCommand command, CancellationToken cancellationToken)
    {
        var userAccount = await _regUserAccountDataStore.GetByUsernameAndPassword(command.UserName, command.Password, cancellationToken);

        if (userAccount is null)
        {
            Log.Information($"UserAccount with UserName={command.UserName} does not exist.");
            
            return Result.Failure<bool>(RegErrors.RegUserAccount.InvalidCredentials);
        }
        
        userAccount.WithoutRefreshToken()
               .WithPassword(command.NewPassword);

        _regUserAccountDataStore.Update(userAccount);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);
        
        Log.Information($"UserAccount with UserName={command.UserName} password changed.");
        
        return true;
    }
}
