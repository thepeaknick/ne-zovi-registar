using AutoMapper;
using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Model;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.User;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.User;

internal sealed class AllUsersQueryHandler : IQueryHandler<AllUsersQuery, List<UserDto>>
{
    private readonly ILogger<AllUsersQueryHandler> _logger;
    private readonly IUserDataStore _userDataStore;
    private readonly  IMapper _mapper;

    public AllUsersQueryHandler(ILogger<AllUsersQueryHandler> logger, IUserDataStore userDataStore, IMapper mapper)
    {
        _logger = logger;
        _userDataStore = userDataStore;
        _mapper = mapper;
    }

    public async Task<Result<List<UserDto>>> Handle(AllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userDataStore.GetAll(request.After, cancellationToken);

        return users.Any()
            ? Result.Failure<List<UserDto>>(RegErrors.User.NotFoundAfter(request.After))
            : _mapper.Map<List<UserDto>>(users);
    }
}
