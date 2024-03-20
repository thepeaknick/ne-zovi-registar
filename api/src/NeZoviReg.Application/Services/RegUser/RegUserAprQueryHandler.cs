using AutoMapper;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Infrastructure.WebClient;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.RegUser;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class RegUserQueryAprHandler : IQueryHandler<RegUserAprQuery, RegUserAprDetailsDto>
{
    private readonly IAprWebClient _aprWebClient;
    private readonly IMapper _mapper;

    public RegUserQueryAprHandler(IAprWebClient aprWebClient, IMapper mapper)
    {
        _aprWebClient = aprWebClient;
        _mapper = mapper;
    }

    public async Task<Result<RegUserAprDetailsDto>> Handle(RegUserAprQuery query, CancellationToken cancellationToken)
    {
        var regUser = await _aprWebClient.GetAprBusinessEntityAsync(query.RegNumber, cancellationToken);

        return regUser is not null
            ? _mapper.Map<RegUserAprDetailsDto>(regUser)
            : Result.Failure<RegUserAprDetailsDto>(RegErrors.RegUser.NotFound(query.RegNumber));
    }
}
