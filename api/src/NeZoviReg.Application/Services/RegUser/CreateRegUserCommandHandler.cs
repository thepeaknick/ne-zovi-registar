using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using NeZoviReg.Abstractions.Email;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Infrastructure.WebClient;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;
using NeZoviReg.Abstractions.Options;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Events;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Domain.Model.Auth;
using Serilog;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class CreateRegUserCommandHandler : ICommandHandler<CreateRegUserCommand, RegUserDto>
{
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IPublisher _publisher;
    private readonly EmailSenderOptions _options;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAprWebClient _aprWebClient;

    public CreateRegUserCommandHandler(IRegUserDataStore regUserDataStore,
        IPublisher publisher,
        IOptionsSnapshot<EmailSenderOptions> options,
        IUnitOfWork unitOfWork,
        IMapper mapper, 
        IAprWebClient aprWebClient)
    {
        _regUserDataStore = regUserDataStore;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _aprWebClient = aprWebClient;
        _options = options.Value;
        _publisher = publisher;
    }

    public async Task<Result<RegUserDto>> Handle(CreateRegUserCommand command, CancellationToken cancellationToken)
    {
        var regUser = Domain.Model.Domain.RegUser.Create(command.CompanyName)
            .WithAddress(command.Address)
            .WithEmail(command.Email)
            .WithRegNumber(command.RegNumber)
            .WithTaxNumber(command.TaxNumber)
            .WithName(command.FirstName, command.LastName)
            .WithRole((int) command.Role)
            .WithUserAccount(Domain.Model.Auth.RegUserAccount.New
                .WithUserName(command.UserName)
                .WithPassword(command.Password));

        await _regUserDataStore.Add(regUser, cancellationToken);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);

        await SendNotification(command, cancellationToken);

        Log.Information($"RegUser with Email={command.Email} added.");

        return _mapper.Map<RegUserDto>(regUser);
    }

    private async Task SendNotification(CreateRegUserCommand command, CancellationToken cancellationToken)
    {
        if (command.Role == RoleType.Trgovac)
        {
            await _publisher.Publish(new RegUserCreatedEvent
            {
                CompanyName = command.CompanyName,
                Email = command.Email,
                Username = command.UserName
            }, cancellationToken);
        }
    }
}