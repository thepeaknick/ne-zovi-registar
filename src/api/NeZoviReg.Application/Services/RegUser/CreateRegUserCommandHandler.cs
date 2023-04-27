using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Shared;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class CreateRegUserCommandHandler : ICommandHandler<CreateRegUserCommand, string>
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

    public async Task<Result<string>> Handle(CreateRegUserCommand request, CancellationToken cancellationToken)
    {
        var rolles = await _authDataStore.GetRollesAsync(request.Roles, cancellationToken);

        var regUser = new Domain.Model.Domain.RegUser(request.UserName, request.Email)
            .AddPassword(request.Password)
            .AddName(request.FirstName, request.LastName)
            .AddRoles(rolles.Select(r => r.Id).ToList());

        await _regUserDataStore.Add(regUser, cancellationToken);

        await _unitOfWork.SaveChangesAsync(request.AppUser, cancellationToken);

        return regUser.FullName;
    }
}
