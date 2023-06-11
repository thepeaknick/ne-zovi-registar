using AutoMapper;
using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;
using NeZoviReg.Abstractions.Shared;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class CreateRegUserCommandHandler : ICommandHandler<CreateRegUserCommand, RegUserDto>
{
    private readonly ILogger<CreateRegUserCommandHandler> _logger;
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IAuthDataStore _authDataStore;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public CreateRegUserCommandHandler(ILogger<CreateRegUserCommandHandler> logger,
        IRegUserDataStore regUserDataStore,
        IAuthDataStore authDataStore,
        IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _regUserDataStore = regUserDataStore;
        _authDataStore = authDataStore;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<RegUserDto>> Handle(CreateRegUserCommand command, CancellationToken cancellationToken)
    {
        var rolles = await _authDataStore.GetRollesAsync(command.Roles, cancellationToken);

        var regUser = new Domain.Model.Domain.RegUser(command.CompanyName, command.UserName)
            .WithAddress(command.Address)
            .WithEmail(command.Email)
            .WithRegNumber(command.RegNumber)
            .WithTaxNumber(command.TaxNumber)
            .WithName(command.FirstName, command.LastName)
            .WithPassword(command.Password)
            .WithRoles(rolles.Select(r => r.Id).ToList());

        await _regUserDataStore.Add(regUser, cancellationToken);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);

        return _mapper.Map<RegUserDto>(regUser);
    }
}
