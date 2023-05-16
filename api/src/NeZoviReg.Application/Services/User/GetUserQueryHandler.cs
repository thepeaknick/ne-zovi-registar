using AutoMapper;
using Microsoft.Extensions.Logging;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Model;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.User;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.User;

internal sealed class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
{
    private readonly ILogger<GetUserQueryHandler> _logger;
    private readonly IUserDataStore _userDataStore;
    private readonly  IMapper _mapper;

    public GetUserQueryHandler(ILogger<GetUserQueryHandler> logger, IUserDataStore userDataStore, IMapper mapper)
    {
        _logger = logger;
        _userDataStore = userDataStore;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userDataStore.GetByPhoneNumber(request.PhoneNumber, cancellationToken);

        return user is null
            ? Result.Failure<UserDto>(RegErrors.User.NotFound(request.PhoneNumber))
            : _mapper.Map<UserDto>(user);
    }
}
