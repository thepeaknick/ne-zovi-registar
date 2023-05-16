using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model;
using NeZoviReg.Abstractions.Shared;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class CreateRegUserCommandHandler : ICommandHandler<CreateRegUserCommand, RegUserDto>
{
    private readonly ILogger<CreateRegUserCommandHandler> _logger;
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IAuthDataStore _authDataStore;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRegUserCommandHandler(ILogger<CreateRegUserCommandHandler> logger,
        IRegUserDataStore regUserDataStore,
        IAuthDataStore authDataStore,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _regUserDataStore = regUserDataStore;
        _authDataStore = authDataStore;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RegUserDto>> Handle(CreateRegUserCommand request, CancellationToken cancellationToken)
    {
        var rolles = await _authDataStore.GetRollesAsync(request.Roles, cancellationToken);

        var regUser = new Domain.Model.Domain.RegUser(request.CompanyName, request.UserName)
            .AddAddress(request.Address)
            .AddRegNumber(request.RegNumber)
            .AddTaxNumber(request.TaxNumber)
            .AddName(request.FirstName, request.LastName)
            .AddPassword(request.Password)
            .AddRoles(rolles.Select(r => r.Id).ToList());

        await _regUserDataStore.Add(regUser, cancellationToken);

        await _unitOfWork.SaveChangesAsync(request.AppUser, cancellationToken);

        return new RegUserDto(regUser.GuidId, regUser.FullName, regUser.Id);
    }
}
