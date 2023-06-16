using AutoMapper;
using MediatR;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using NeZoviReg.Abstractions.Shared.Events;
using Serilog;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class ModifyRegUserCommandHandler : ICommandHandler<ModifyRegUserCommand, RegUserDto>
{
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ModifyRegUserCommandHandler(IRegUserDataStore regUserDataStore,
        IPublisher publisher,
        IUnitOfWork unitOfWork, IMapper mapper)
    {
        _regUserDataStore = regUserDataStore;
        _publisher = publisher;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<RegUserDto>> Handle(ModifyRegUserCommand command, CancellationToken cancellationToken)
    {
        var regUser = await _regUserDataStore.GetByGuidId(command.RegUserId, cancellationToken);

        if (regUser is null)
        {
            Log.Information($"RegUser with RegUserId={command.RegUserId} does not exist.");
            
            return Result.Failure<RegUserDto>(RegErrors.RegUser.NotFound(command.RegUserId));
        }

        regUser
            .WithCompanyName(command.CompanyName)
            .WithEmail(command.Email)
            .WithAddress(command.Address)
            .WithRegNumber(command.RegNumber)
            .WithTaxNumber(command.TaxNumber)
            .WithName(command.FirstName, command.LastName)
            .WithUserName(command.UserName)
            .WithRole(command.Role);

        _regUserDataStore.Update(regUser);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);

        await _publisher.Publish(new RegUserModifiedEvent
        {
            RegUserId = regUser.GuidId
        }, cancellationToken);
        
        Log.Information($"RegUser with RegUserId={command.RegUserId} modified.");
        
       return _mapper.Map<RegUserDto>(regUser);
    }
}
