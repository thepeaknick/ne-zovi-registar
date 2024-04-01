using MediatR;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Events;
using Serilog;

namespace NeZoviReg.Application.Services.UserAccount;

internal sealed class ChangeUserAccountPassCommandHandler : ICommandHandler<ChangePassCommand, bool>
{
    private readonly IUserAccountDataStore _userAccountDataStore;
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeUserAccountPassCommandHandler(IUserAccountDataStore userAccountDataStore,
        IPublisher publisher,
        IUnitOfWork unitOfWork)
    {
        _userAccountDataStore = userAccountDataStore;
        _publisher = publisher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(ChangePassCommand command, CancellationToken cancellationToken)
    {
        var userAccount = await _userAccountDataStore.GetByUsernameAndPassword(command.UserName, command.Password, cancellationToken);

        if (userAccount is null)
        {
            Log.Information($"UserAccount with UserName={command.UserName} does not exist.");
            
            return Result.Failure<bool>(RegErrors.UserAccount.InvalidCredentials);
        }
        
        userAccount.WithoutRefreshToken()
               .WithPassword(command.NewPassword);

        _userAccountDataStore.Update(userAccount);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);
        
        Log.Information($"UserAccount with UserName={command.UserName} password changed.");
        
        return true;
    }
}
