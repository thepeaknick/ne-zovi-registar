using AutoMapper;
using NeZoviReg.Abstractions.Extensions.Paging;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Model.User;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.User;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.User;

internal sealed class AllUsersQueryHandler : IQueryHandler<AllUsersQuery, PagedList<UserDto>>
{
    private readonly IUserDataStore _userDataStore;
    private readonly IMapper _mapper;

    public AllUsersQueryHandler(IUserDataStore userDataStore, IMapper mapper)
    {
        _userDataStore = userDataStore;
        _mapper = mapper;
    }

    public async Task<Result<PagedList<UserDto>>> Handle(AllUsersQuery query, CancellationToken cancellationToken)
    {
        var all = await _userDataStore.GetAll(query.After, query.PageInfo, cancellationToken);

        if (all.Items.Any())
        {
            var users = _mapper.Map<List<UserDto>>(all.Items);
            return new PagedList<UserDto>(users, all.PageInfo);
        }
        return Result.Failure<PagedList<UserDto>>(RegErrors.User.NotFoundAfter(query.After));
    }
}
