using AutoMapper;
using NeZoviReg.Abstractions.Extensions.Paging;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Model.User;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.User;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.User;

internal sealed class AllUsersQueryHandler : IQueryHandler<AllUsersQuery, PagedList<UserInfoDto>>
{
    private readonly IUserDataStore _userDataStore;
    private readonly IMapper _mapper;

    public AllUsersQueryHandler(IUserDataStore userDataStore, IMapper mapper)
    {
        _userDataStore = userDataStore;
        _mapper = mapper;
    }

    public async Task<Result<PagedList<UserInfoDto>>> Handle(AllUsersQuery query, CancellationToken cancellationToken)
    {
        var all = await _userDataStore.GetAll(query.After, query.PageInfo, cancellationToken);

        return all.Items.Any()
            ? new PagedList<UserInfoDto>(_mapper.Map<List<UserInfoDto>>(all.Items), all.PageInfo)
            : Result.Failure<PagedList<UserInfoDto>>(RegErrors.User.NotFoundAfter(query.After));

    }
}
