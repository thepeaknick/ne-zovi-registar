using MediatR;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Events;
using Serilog;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class ResetUserAccountPassCommandHandler : ICommandHandler<ResetPassCommand, bool>
{
    private readonly IUserAccountDataStore _userAccountDataStore;
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;

    public ResetUserAccountPassCommandHandler(IUserAccountDataStore userAccountDataStore,
        IPublisher publisher,
        IUnitOfWork unitOfWork)
    {
        _userAccountDataStore = userAccountDataStore;
        _publisher = publisher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(ResetPassCommand command, CancellationToken cancellationToken)
    {
        var userAccount = await _userAccountDataStore.GetByEmail(command.Email, cancellationToken);

        if (userAccount is null)
        {
            Log.Information($"UserAccount with Email={command.Email} does not exist.");

            return Result.Failure<bool>(RegErrors.RegUser.Unknown);
        }
        
        if (userAccount.ForgotPasswordToken != command.Token
            || userAccount.ForgotPasswordTokenExpirationTime < DateTime.Now)
        {
            return Result.Failure<bool>(RegErrors.Token.ForgotPasswordTokenExpiredOrNotValid);
        }
        
        userAccount.WithPassword(command.Password);

        _userAccountDataStore.Update(userAccount);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);

        await _publisher.Publish(new UserAccountModifiedEvent
        {
            UserAccountId = userAccount.GuidId
        }, cancellationToken);

        Log.Information($"UserAccount with Email={command.Email} reset password successfully.");
        
        return true;
    }
}
