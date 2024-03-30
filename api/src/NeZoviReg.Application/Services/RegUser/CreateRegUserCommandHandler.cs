using AutoMapper;
using Microsoft.Extensions.Options;
using NeZoviReg.Abstractions.Email;
using NeZoviReg.Abstractions.Infrastructure.DataStores;
using NeZoviReg.Abstractions.Infrastructure.DataStores.Domain;
using NeZoviReg.Abstractions.Infrastructure.WebClient;
using NeZoviReg.Abstractions.Messaging;
using NeZoviReg.Abstractions.Messaging.Domain.Commands.RegUser;
using NeZoviReg.Abstractions.Messaging.Domain.Model.RegUser;
using NeZoviReg.Abstractions.Options;
using NeZoviReg.Abstractions.Shared;
using NeZoviReg.Abstractions.Shared.Model.Auth.Enum;
using NeZoviReg.Domain.Model.Auth;
using Serilog;

namespace NeZoviReg.Application.Services.RegUser;

internal sealed class CreateRegUserCommandHandler : ICommandHandler<CreateRegUserCommand, RegUserDto>
{
    private readonly IRegUserDataStore _regUserDataStore;
    private readonly IEmailSender _emailSender;
    private readonly EmailSenderOptions _options;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAprWebClient _aprWebClient;

    public CreateRegUserCommandHandler(IRegUserDataStore regUserDataStore,
        IEmailSender emailSender,
        IOptionsSnapshot<EmailSenderOptions> options,
        IUnitOfWork unitOfWork,
        IMapper mapper, 
        IAprWebClient aprWebClient)
    {
        _regUserDataStore = regUserDataStore;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _aprWebClient = aprWebClient;
        _options = options.Value;
        _emailSender = emailSender;
    }

    public async Task<Result<RegUserDto>> Handle(CreateRegUserCommand command, CancellationToken cancellationToken)
    {
        var regUser = Domain.Model.Domain.RegUser.Create(command.CompanyName, command.UserName)
            .WithAddress(command.Address)
            .WithEmail(command.Email)
            .WithRegNumber(command.RegNumber)
            .WithTaxNumber(command.TaxNumber)
            .WithName(command.FirstName, command.LastName)
            .WithPassword(command.Password)
            .WithRole((int) command.Role)
            .WithUserAccount(UserAccount.New.WithUserName(command.UserName).WithPassword(command.Password));

        await _regUserDataStore.Add(regUser, cancellationToken);

        await _unitOfWork.SaveChangesAsync(command.AppUser, cancellationToken);

        if (command.Role == RoleType.Trgovac)
        {
            await _emailSender.SendEmailAsync(_options.EmailTo,
                $"Trgovac: {command.CompanyName}. je registrovan.",
                $"Korisničko ime: {command.UserName}",
                cancellationToken: cancellationToken);
        }

        Log.Information($"RegUser with Email={command.Email} added.");

        return _mapper.Map<RegUserDto>(regUser);
    }
}