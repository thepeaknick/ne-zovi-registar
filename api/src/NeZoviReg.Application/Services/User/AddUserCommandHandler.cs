using AutoMapper;
using NeZoviReg.Abstractions.Extensions.Domain;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.User;
using NeZoviReg.Abstractions.Messaging.Domain.Model.User;
using NeZoviReg.Abstractions.Shared;
using Serilog;

namespace NeZoviReg.Application.Services.User;

internal sealed class AddUserCommandHandler : ICommandHandler<AddUserCommand, List<UserDto>>
{
    private readonly IUserDataStore _userDataStore;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public AddUserCommandHandler(IUserDataStore userDataStore, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userDataStore = userDataStore;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<UserDto>>> Handle(AddUserCommand command, CancellationToken cancellationToken)
    {
        var result = new List<UserDto>();
        
        foreach (var phoneNumber in command.PhoneNumbers)
        {
            var user =
                new Domain.Model.Domain.User(command.FirstName, command.LastName, phoneNumber.FormatPhoneNumber())
                    .AddJmbg(command.Jmbg)
                    .AddOperator(command.OperatorId);

            await _userDataStore.AddAsync(user, cancellationToken);

            await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);
            
            Log.Information($"PhoneNumber={phoneNumber} added.");
            
            result.Add(_mapper.Map<UserDto>(user));
        }

        return result;
    }
}
