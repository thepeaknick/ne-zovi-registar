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
    private readonly IRegUserAccountDataStore _regUserAccountDataStore;
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;

    public ResetUserAccountPassCommandHandler(IRegUserAccountDataStore regUserAccountDataStore,
        IPublisher publisher,
        IUnitOfWork unitOfWork)
    {
        _regUserAccountDataStore = regUserAccountDataStore;
        _publisher = publisher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(ResetPassCommand command, CancellationToken cancellationToken)
    {
        var userAccount = await _regUserAccountDataStore.GetByEmail(command.Email, cancellationToken);

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

        _regUserAccountDataStore.Update(userAccount);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);

        Log.Information($"UserAccount with Email={command.Email} reset password successfully.");
        
        return true;
    }
}
