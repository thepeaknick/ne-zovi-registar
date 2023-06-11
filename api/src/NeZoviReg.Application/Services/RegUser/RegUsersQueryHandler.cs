using AutoMapper;
using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Model;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.RegUser;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class RegUsersQueryHandler : IQueryHandler<RegUsersQuery, List<RegUserDto>>
{
    private readonly ILogger<RegUsersQueryHandler> _logger;
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IMapper _mapper;

    public RegUsersQueryHandler(ILogger<RegUsersQueryHandler> logger, IRegUserDataStore regUserDataStore, IMapper mapper)
    {
        _logger = logger;
        _regUserDataStore = regUserDataStore;
        _mapper = mapper;
    }

    public async Task<Result<List<RegUserDto>>> Handle(RegUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await _regUserDataStore.GetByRole(query.Role, cancellationToken);

        return users.Any()
            ? _mapper.Map<List<RegUserDto>>(users)
            : Result.Failure<List<RegUserDto>>(RegErrors.RegUser.RolesNotFound(query.Role.ToString()));
    }
}
