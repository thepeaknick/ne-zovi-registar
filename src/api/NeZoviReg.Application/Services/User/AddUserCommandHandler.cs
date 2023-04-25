using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands;
using NeZoviReg.Abstractions.Shared;

namespace NeZoviReg.Application.Services.User;

internal sealed class AddUserCommandHandler : ICommandHandler<AddUserCommand, string>
{
    private readonly ILogger<AddUserCommandHandler> _logger;
    private readonly IUserDataStore _userDataStore;
    private readonly IUnitOfWork _unitOfWork;

    public AddUserCommandHandler(ILogger<AddUserCommandHandler> logger, IUserDataStore userDataStore, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _userDataStore = userDataStore;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var user =
            new Domain.Model.Domain.User(request.FirstName, request.LastName, request.PhoneNumber, request.Jmbg);

        _userDataStore.Add(user);

        await _unitOfWork.SaveChangesAsync(request.AppUser, cancellationToken);

        return user.FullName;
    }
}
