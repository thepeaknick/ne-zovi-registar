using AutoMapper;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Model.User;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.User;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.User;

internal sealed class AllUsersQueryHandler : IQueryHandler<AllUsersQuery, List<UserDto>>
{
    private readonly IUserDataStore _userDataStore;
    private readonly IMapper _mapper;

    public AllUsersQueryHandler(IUserDataStore userDataStore, IMapper mapper)
    {
        _userDataStore = userDataStore;
        _mapper = mapper;
    }

    public async Task<Result<List<UserDto>>> Handle(AllUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await _userDataStore.GetAll(query.After, cancellationToken);

        return users.Any()
            ? _mapper.Map<List<UserDto>>(users).ToList()
            : Result.Failure<List<UserDto>>(RegErrors.User.NotFoundAfter(query.After));
    }
}
