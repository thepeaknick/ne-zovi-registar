using MediatR;
using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Events;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class ModifyRegUserCommandHandler : ICommandHandler<ModifyRegUserCommand, RegUserDto>
{
    private readonly ILogger<ModifyRegUserCommandHandler> _logger;
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;

    public ModifyRegUserCommandHandler(ILogger<ModifyRegUserCommandHandler> logger,
        IRegUserDataStore regUserDataStore,
        IPublisher publisher,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _regUserDataStore = regUserDataStore;
        _publisher = publisher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RegUserDto>> Handle(ModifyRegUserCommand request, CancellationToken cancellationToken)
    {
        var regUser = await _regUserDataStore.GetByGuidId(request.RegUserId, cancellationToken);

        if (regUser is null)
        {
            return Result.Failure<RegUserDto>(RegErrors.RegUser.NotFound(request.RegUserId));
        }

        regUser
            .WithCompanyName(request.CompanyName)
            .WithAddress(request.Address)
            .WithRegNumber(request.RegNumber)
            .WithTaxNumber(request.TaxNumber)
            .WithName(request.FirstName, request.LastName)
            .WithUserName(request.UserName)
            .WithRoles(request.Roles);

        _regUserDataStore.Update(regUser);

        await _unitOfWork.SaveChangesAsync(request.AppUser, cancellationToken);

        await _publisher.Publish(new RegUserModifiedEvent
        {
            RegUserId = regUser.GuidId
        }, cancellationToken);

        return new RegUserDto(regUser.GuidId, regUser.FullName, regUser.Id);
    }
}
