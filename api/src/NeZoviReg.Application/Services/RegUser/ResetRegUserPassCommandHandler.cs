using MediatR;
using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Events;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class ResetRegUserPassCommandHandler : ICommandHandler<ResetPassCommand, bool>
{
    private readonly ILogger<ResetRegUserPassCommandHandler> _logger;
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;

    public ResetRegUserPassCommandHandler(ILogger<ResetRegUserPassCommandHandler> logger,
        IRegUserDataStore regUserDataStore,
        IPublisher publisher,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _regUserDataStore = regUserDataStore;
        _publisher = publisher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(ResetPassCommand command, CancellationToken cancellationToken)
    {
        var regUser = await _regUserDataStore.GetByEmail(command.Email, cancellationToken);

        if (regUser is null)
        {
            _logger.LogInformation($"RegUser with Email={command.Email} does not exist.");

            return Result.Failure<bool>(RegErrors.RegUser.Unknown);
        }
        
        if (regUser.ForgotPasswordToken != command.Token
            || regUser.ForgotPasswordTokenExpirationTime < DateTime.Now)
        {
            return Result.Failure<bool>(RegErrors.Token.ForgotPasswordTokenExpiredOrNotValid);
        }
        
        regUser.WithPassword(command.Password);

        _regUserDataStore.Update(regUser);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);

        await _publisher.Publish(new RegUserModifiedEvent
        {
            RegUserId = regUser.GuidId
        }, cancellationToken);

        return true;
    }
}
