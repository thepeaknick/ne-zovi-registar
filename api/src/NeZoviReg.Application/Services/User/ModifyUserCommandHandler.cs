using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.User;
using NeZoviReg.Abstractions.Messaging.Domain.Model;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.User;

internal sealed class ModifyUserCommandHandler : ICommandHandler<ModifyUserCommand, UserDto>
{
    private readonly ILogger<ModifyUserCommandHandler> _logger;
    private readonly IUserDataStore _userDataStore;
    private readonly IUnitOfWork _unitOfWork;

    public ModifyUserCommandHandler(ILogger<ModifyUserCommandHandler> logger, IUserDataStore userDataStore, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _userDataStore = userDataStore;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDto>> Handle(ModifyUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userDataStore.GetByPhoneNumber(request.PhoneNumber, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserDto>(RegErrors.User.NotFound(request.PhoneNumber));
        }

        user.AddName(request.FirstName, request.LastName)
            .AddJmbg(request.Jmbg)
            .AddPhoneNumber(request.NewPhoneNumber);

        _userDataStore.Update(user);

        await _unitOfWork.SaveChangesAsync(request.AppUser, cancellationToken);

        return new UserDto(user.PhoneNumber, $"{user.ModifiedOn.GetValueOrDefault():dd.MM.yy HH:mm}");
    }
}
