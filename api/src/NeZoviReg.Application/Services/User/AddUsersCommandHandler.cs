using AutoMapper;
using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.User;
using NeZoviReg.Abstractions.Messaging.Domain.Model.User;
using NeZoviReg.Abstractions.Shared;

namespace NeZoviReg.Application.Services.User;

internal sealed class AddUsersCommandHandler : ICommandHandler<AddUsersCommand, List<UserDto>>
{
    private readonly ILogger<AddUsersCommandHandler> _logger;
    private readonly IUserDataStore _userDataStore;
    private readonly IMapper _mapper;

    public AddUsersCommandHandler(ILogger<AddUsersCommandHandler> logger,
        IUserDataStore userDataStore,
        IMapper mapper)
    {
        _logger = logger;
        _userDataStore = userDataStore;
        _mapper = mapper;
    }

    public async Task<Result<List<UserDto>>> Handle(AddUsersCommand command, CancellationToken cancellationToken)
    {
        var users = _mapper.Map<List<Domain.Model.Domain.User>>(command.Users);
        users.ForEach(u => u.AddCreation(command.AppUser));

        await _userDataStore.BulkAddAsync(users, cancellationToken);

        return _mapper.Map<List<UserDto>>(users);
    }
}