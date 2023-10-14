using AutoMapper;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.User;
using NeZoviReg.Abstractions.Messaging.Domain.Model.User;
using NeZoviReg.Abstractions.Shared;
using Serilog;

namespace NeZoviReg.Application.Services.User;

internal sealed class AddUsersCommandHandler : ICommandHandler<AddUsersCommand, List<UserDto>>
{
    private readonly IUserDataStore _userDataStore;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddUsersCommandHandler(IUserDataStore userDataStore,
        IMapper mapper, IUnitOfWork unitOfWork)
    {
        _userDataStore = userDataStore;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<UserDto>>> Handle(AddUsersCommand command, CancellationToken cancellationToken)
    {
        var users = _mapper.Map<List<Domain.Model.Domain.User>>(command.Users);
        users.ForEach(u => u.AddCreation(command.AppUser));

        await _userDataStore.BulkAddAsync(users, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);
        
        Log.Information("Bulk add phoneNumbers finished.");

        return _mapper.Map<List<UserDto>>(users);
    }
}