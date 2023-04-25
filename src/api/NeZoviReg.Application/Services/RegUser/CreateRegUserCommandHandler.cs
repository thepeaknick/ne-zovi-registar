using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Application.Infrastructure.DataStores;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class CreateRegUserCommandHandler : ICommandHandler<CreateRegUserCommand, string>
{
    private readonly ILogger<CreateRegUserCommandHandler> _logger;
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRegUserCommandHandler(ILogger<CreateRegUserCommandHandler> logger, IRegUserDataStore regUserDataStore, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _regUserDataStore = regUserDataStore;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(CreateRegUserCommand request, CancellationToken cancellationToken)
    {
        var regUser = new Domain.Model.Domain.RegUser(request.UserName, request.Email)
            .AddPassword(request.Password)
            .AddName(request.FirstName, request.LastName);

        _regUserDataStore.Add(regUser);

        await _unitOfWork.SaveChangesAsync(request.AppUser, cancellationToken);

        return regUser.Username;
    }
}
