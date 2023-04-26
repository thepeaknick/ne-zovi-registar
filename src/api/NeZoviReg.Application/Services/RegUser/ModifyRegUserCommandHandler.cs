using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class ModifyRegUserCommandHandler : ICommandHandler<ModifyRegUserCommand, string>
{
    private readonly ILogger<ModifyRegUserCommandHandler> _logger;
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IAuthDataStore _authDataStore;
    private readonly IUnitOfWork _unitOfWork;

    public ModifyRegUserCommandHandler(ILogger<ModifyRegUserCommandHandler> logger,
        IRegUserDataStore regUserDataStore,
        IAuthDataStore authDataStore,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _regUserDataStore = regUserDataStore;
        _authDataStore = authDataStore;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(ModifyRegUserCommand request, CancellationToken cancellationToken)
    {
        var regUser = await _regUserDataStore.GetByGuidId(request.RegUserId, cancellationToken);

        if (regUser is null)
        {
            return Result.Failure<string>(RegErrors.RegUser.NotFound(request.RegUserId));
        }

        regUser
            .AddEmail(request.Email)
            .AddName(request.FirstName, request.LastName)
            .AddPassword(request.Password)
            .AddRoles(request.Roles);

        _regUserDataStore.Update(regUser);

        await _unitOfWork.SaveChangesAsync(request.AppUser, cancellationToken);

        return regUser.FullName;
    }
}
