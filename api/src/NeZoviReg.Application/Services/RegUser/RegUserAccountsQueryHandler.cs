using AutoMapper;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Auth;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model.UserAccount;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.RegUser;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class RegUserAccontsQueryHandler : IQueryHandler<RegUserAccountsQuery, RegUserAccountsDto>
{
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IMapper _mapper;

    public RegUserAccontsQueryHandler(IRegUserDataStore regUserDataStore, IMapper mapper)
    {
        _regUserDataStore = regUserDataStore;
        _mapper = mapper;
    }

    public async Task<Result<RegUserAccountsDto>> Handle(RegUserAccountsQuery query, CancellationToken cancellationToken)
    {
        var regUser = await _regUserDataStore.GetWithAccountsByGuidId(query.RegUserId, cancellationToken);

        return regUser is not null
            ? _mapper.Map<RegUserAccountsDto>(regUser)
            : Result.Failure<RegUserAccountsDto>(RegErrors.RegUser.NotFound(query.RegUserId));
    }
}
