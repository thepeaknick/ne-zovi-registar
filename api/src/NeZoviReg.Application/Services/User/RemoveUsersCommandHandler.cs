using AutoMapper;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.User;
using NeZoviReg.Abstractions.Messaging.Domain.Model.User;
using NeZoviReg.Abstractions.Shared;
using Serilog;

namespace NeZoviReg.Application.Services.User;

internal sealed class RemoveUsersCommandHandler : ICommandHandler<RemoveUsersCommand, bool>
{
    private readonly IUserDataStore _userDataStore;
    
    public RemoveUsersCommandHandler(IUserDataStore userDataStore)
    {
        _userDataStore = userDataStore;
    }

    public async Task<Result<bool>> Handle(RemoveUsersCommand command, CancellationToken cancellationToken)
    {
        await _userDataStore.BulkRemoveAsync(command.PhoneNumbers, cancellationToken);

        Log.Information("Bulk remove phoneNumbers finished.");

        return true;
    }
}