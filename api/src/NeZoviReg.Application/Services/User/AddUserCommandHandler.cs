using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.User;
using NeZoviReg.Abstractions.Messaging.Domain.Model;
using NeZoviReg.Abstractions.Shared;

namespace NeZoviReg.Application.Services.User;

internal sealed class AddUserCommandHandler : ICommandHandler<AddUserCommand, UserDto>
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

    public async Task<Result<UserDto>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var user =
            new Domain.Model.Domain.User(request.FirstName, request.LastName, request.PhoneNumber)
                .AddJmbg(request.Jmbg)
                .AddOperator(request.OperatorId);

        await _userDataStore.AddAsync(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync(request.AppUser, cancellationToken);

        return new UserDto(user.PhoneNumber, $"{user.CreatedOn:dd.MM.yy HH:mm}");
    }
}
