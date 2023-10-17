using AutoMapper;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Model.User;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.User;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.User;

internal sealed class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
{
    private readonly IUserDataStore _userDataStore;
    private readonly  IMapper _mapper;

    public GetUserQueryHandler(IUserDataStore userDataStore, IMapper mapper)
    {
        _userDataStore = userDataStore;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _userDataStore.GetByPhoneNumber(query.PhoneNumber, cancellationToken);

        return (user is null || !user.IsActive)
            ? Result.Failure<UserDto>(RegErrors.User.NotFound(query.PhoneNumber))
            : _mapper.Map<UserDto>(user);
    }
}
