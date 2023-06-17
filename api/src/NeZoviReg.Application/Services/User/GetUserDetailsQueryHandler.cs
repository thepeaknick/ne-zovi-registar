using AutoMapper;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Model.User;
using NeZoviReg.Abstractions.Messaging.Domain.Queries.User;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Errors;

namespace NeZoviReg.Application.Services.User;

internal sealed class GetUserDetailsQueryHandler : IQueryHandler<GetUserDetailsQuery, UserDetailsDto>
{
    private readonly IUserDataStore _userDataStore;
    private readonly  IMapper _mapper;

    public GetUserDetailsQueryHandler(IUserDataStore userDataStore, IMapper mapper)
    {
        _userDataStore = userDataStore;
        _mapper = mapper;
    }

    public async Task<Result<UserDetailsDto>> Handle(GetUserDetailsQuery query, CancellationToken cancellationToken)
    {
        var user = await _userDataStore.GetByPhoneNumber(query.PhoneNumber, cancellationToken);

        return user is null
            ? Result.Failure<UserDetailsDto>(RegErrors.User.NotFound(query.PhoneNumber))
            : _mapper.Map<UserDetailsDto>(user);
    }
}
