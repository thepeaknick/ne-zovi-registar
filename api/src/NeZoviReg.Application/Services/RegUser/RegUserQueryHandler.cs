using AutoMapper;
using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.RegUser;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class RegUserQueryHandler : IQueryHandler<RegUserQuery, RegUserDetailsDto>
{
    private readonly ILogger<RegUserQueryHandler> _logger;
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IMapper _mapper;

    public RegUserQueryHandler(ILogger<RegUserQueryHandler> logger, IRegUserDataStore regUserDataStore, IMapper mapper)
    {
        _logger = logger;
        _regUserDataStore = regUserDataStore;
        _mapper = mapper;
    }

    public async Task<Result<RegUserDetailsDto>> Handle(RegUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _regUserDataStore.GetByGuidId(query.RegUserId, cancellationToken);

        return user is not null
            ? _mapper.Map<RegUserDetailsDto>(user)
            : Result.Failure<RegUserDetailsDto>(RegErrors.RegUser.NotFound(query.RegUserId));
    }
}
