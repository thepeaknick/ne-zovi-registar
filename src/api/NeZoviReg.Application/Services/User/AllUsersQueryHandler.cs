using AutoMapper;
using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.User;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.User;

internal sealed class AllUsersQueryHandler : IQueryHandler<AllUsersQuery, List<string>>
{
    private readonly ILogger<AllUsersQueryHandler> _logger;
    private readonly IUserDataStore _userDataStore;
    private readonly IMapper _mapper;

    public AllUsersQueryHandler(ILogger<AllUsersQueryHandler> logger, IUserDataStore userDataStore, IMapper mapper)
    {
        _logger = logger;
        _userDataStore = userDataStore;
        _mapper = mapper;
    }

    public async Task<Result<List<string>>> Handle(AllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userDataStore.GetAll(request.After, cancellationToken);

        return users.Any()
            ? users.Select(x => x.PhoneNumber).ToList()
            : Result.Failure<List<string>>(RegErrors.User.NotFoundAfter(request.After));
    }
}
