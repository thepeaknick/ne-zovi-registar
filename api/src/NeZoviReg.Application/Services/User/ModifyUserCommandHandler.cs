using AutoMapper;
using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.User;
using NeZoviReg.Abstractions.Messaging.Domain.Model.User;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;
using Serilog;

namespace NeZoviReg.Application.Services.User;

internal sealed class ModifyUserCommandHandler : ICommandHandler<ModifyUserCommand, UserDto>
{
    private readonly IUserDataStore _userDataStore;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public ModifyUserCommandHandler(IUserDataStore userDataStore, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userDataStore = userDataStore;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(ModifyUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userDataStore.GetByPhoneNumber(command.PhoneNumber, cancellationToken);

        if (user is null)
        {
            Log.Information($"PhoneNumber={command.PhoneNumber} does not exist.");
            
            return Result.Failure<UserDto>(RegErrors.User.NotFound(command.PhoneNumber));
        }

        user.AddName(command.FirstName, command.LastName)
            .AddJmbg(command.Jmbg)
            .AddPhoneNumber(command.NewPhoneNumber);

        _userDataStore.Update(user);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);

        Log.Information($"PhoneNumber={command.PhoneNumber} modified.");
        
        return _mapper.Map<UserDto>(user);
    }
}
