using AutoMapper;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Model.UserAccount;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.RegUser;
using NeZoviReg.Abstractions.Shared;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class RegUserAccontsQueryHandler : IQueryHandler<RegUserAccountsQuery, List<UserAccountData>>
{
    private readonly IRegUserAccountDataStore _regRegUserDataStore;
    private readonly IMapper _mapper;

    public RegUserAccontsQueryHandler(IRegUserAccountDataStore regRegUserDataStore, IMapper mapper)
    {
        _regRegUserDataStore = regRegUserDataStore;
        _mapper = mapper;
    }

    public async Task<Result<List<UserAccountData>>> Handle(RegUserAccountsQuery query, CancellationToken cancellationToken)
    {
        var accounts = await _regRegUserDataStore.GetUserAccounts(query.RegUserId, cancellationToken);

       return _mapper.Map<List<UserAccountData>>(accounts);
    }
}
